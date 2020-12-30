using Godot;

namespace BattleTowers
{
	public interface ITower
	{
		string Description { get; }
		int Life { get; }
		AnimationPlayer Anim { get; }
		Area2D HitArea { get; }
		
		Combo[] Combos { get; }
		int Force(Attack attack);
	}

	public class Combo
	{
		public string Key { get; set; }
		public Attack[] Activation { get; set; }
		public int Force { get; set; }
	}
}
