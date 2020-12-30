using Godot;

namespace BattleTowers.Towers
{
	public class TorreEiffel : KinematicBody2D, ITower
	{
		public string Description => "Torre Eiffel";
		public int Life => 100;
		public AnimationPlayer Anim => GetNode<AnimationPlayer>("AnimationPlayer");
		public Area2D HitArea => GetNode<Area2D>("Area2D");

		public Combo[] Combos => new[]
		{
			new Combo
			{
				Key = "combo_rotation", 
				Activation = new[] {Attack.Kick, Attack.Punch, Attack.Kick, Attack.Punch},
				Force = 25,
			},
		};
		
		public int Force(Attack attack) =>
			attack switch
			{
				Attack.Punch => 10,
				Attack.Kick => 15,
				_ => 0,
			};
	}
}
