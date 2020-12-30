using Godot;

public class Game : Node2D
{
	public override void _Ready()
	{
		var eifel = ResourceLoader.Load<PackedScene>("res://Players/TorreEiffel.tscn");

		var player1 = GetNode<Player>("Players/Player1");
		var player2 = GetNode<Player>("Players/Player2");

		player1.Initialize(eifel.Instance());
		player2.Initialize(eifel.Instance());

		var hud = GetNode<Hud>("Hud");
		hud.StartBattle(player1, player2, 90);
	}

	public override void _Process(float delta)
	{
		var movement = 0;
		if (Input.IsKeyPressed((int) KeyList.A))
		{
			movement -= 300;
		}
		
		if (Input.IsKeyPressed((int) KeyList.D))
		{
			movement += 300;
		}
		
		var player1 = GetNode<Position2D>("Players/Player2").GetChild<KinematicBody2D>(0);
		player1.MoveAndCollide(new Vector2(movement * delta, 0));
		
		// if (movement != 0)
		// {
		// 	((IPlayer) player1).Walk();
		// }
		//
		// if (Input.IsKeyPressed((int) KeyList.O))
		// {
		// 	((IPlayer) player1).Punch();
		// }
		// if (Input.IsKeyPressed((int) KeyList.P))
		// {
		// 	((IPlayer) player1).Kick();
		// }
	}
}
