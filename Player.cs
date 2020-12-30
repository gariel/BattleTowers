using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace BattleTowers
{
	public class Player : Position2D
	{
		public int CurrentLife { get; set; }
		public int FullLife => _tower?.Life ?? 100;
		public string Description => _tower?.Description ?? "";
	
		[Export]
		public bool FacingLeft
		{
			get => _facingLeft;
			set
			{
				_facingLeft = value;
				Scale = new Vector2(value ? -1 : 1, 1);
			}
		}

		private ITower? _tower;
		private bool _facingLeft;
		private bool _attacking;
		private List<(DateTime, Attack)> AttacksForCombo;

		public Player()
		{
			AttacksForCombo = new List<(DateTime, Attack)>();
		}

		public void Initialize(Node playable)
		{
			AddChild(playable);
			_tower = (ITower) playable;
			_tower.Anim.Play("idle");
			_tower.Anim.Connect("animation_finished", this, nameof(AnimationFinished));
		}
	
		public override void _Ready()
		{
		}
	
		public void Walk()
		{
			if (_attacking) return;
			_tower?.Anim.Play("walk");
		}
	
		public void Jump()
		{
			if (_attacking) return;
			_tower?.Anim.Play("jump");
		}

		public void Attack(Attack attack)
		{
			if (_tower == null || _attacking)
				return;
			
			// arruma a lista de ataques
			AttacksForCombo.Add((DateTime.Now, attack));
			var limit = DateTime.Now - TimeSpan.FromSeconds(2);
			
			while (AttacksForCombo.Count > 0)
			{
				var (date, _) = AttacksForCombo.FirstOrDefault();
				if (date.CompareTo(limit) == -1)
					AttacksForCombo.RemoveAt(0);
				else
					break;
			}
			
			// acha se fez algum combo
			var invoke = attack.ToString().ToLower();
			foreach (var combo in _tower.Combos)
			{
				var activationCount = combo.Activation.Length;
				var activator = AttacksForCombo
					.Skip(AttacksForCombo.Count - activationCount)
					.Select(i => i.Item2);

				if (activator.SequenceEqual(combo.Activation))
				{
					invoke = combo.Key;
					AttacksForCombo.Clear();
					break;
				}
			}
			
			_attacking = true;
			_tower.Anim.Stop();
			_tower.Anim.Play(invoke);
		}

		public void AnimationFinished(string name)
		{
			_attacking = false;
			_tower?.Anim.Play("idle");
		}
	}
}
