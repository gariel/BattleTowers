using Godot;

public class Tower : KinematicBody2D
{
	[Export] public string Description { get; set; } = "Tower Description";
	[Export] public int Life { get; set; } = 100;
	[Export] public NodePath AnimationPlayer { get; set; } = "AnimationPlayer";
	
	public AnimationPlayer Anim => GetNode<AnimationPlayer>(AnimationPlayer);
}
