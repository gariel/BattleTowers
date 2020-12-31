using Godot;

namespace BattleTowers.Towers
{
	public class CristoRedentor : KinematicBody2D, ITower
	{
		public string Description => "Cristo Redentor";
		public int Life => 120;
		public AnimationPlayer Anim => GetNode<AnimationPlayer>("AnimationPlayer");
		public Area2D HitArea => GetNode<Area2D>("Area2D");

		public Combo[] Combos => new[]
		{
			new Combo
			{
				Key = "combo_sky", 
				Activation = new[] {Attack.Punch, Attack.Punch, Attack.Kick, Attack.Punch},
				Force = 20,
			},
		};

		public int Force(Attack attack)
			=> attack switch
			{
				Attack.Punch => 15,
				Attack.Kick => 10,
				_ => 0,
			};
	}
}
