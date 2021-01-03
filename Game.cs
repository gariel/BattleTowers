using Godot;

namespace BattleTowers
{
	public class Game : Node2D
	{
		private Player _player1;
		private Player _player2;

		public override void _Ready()
		{
			var eifel = ResourceLoader.Load<PackedScene>("res://Towers/TorreEiffel.tscn");
			var redentor = ResourceLoader.Load<PackedScene>("res://Towers/CristoRedentor.tscn");

			_player1 = GetNode<Player>("Players/Player1");
			_player2 = GetNode<Player>("Players/Player2");

			// _player1.Initialize(eifel.Instance()); _player2.Initialize(redentor.Instance());
			_player1.Initialize(redentor.Instance()); _player2.Initialize(eifel.Instance());

			var hud = GetNode<Hud>("Hud");
			hud.StartBattle(_player1, _player2, 90);
		}

		public override void _Process(float delta)
		{
			var movement = 0;
			if (Input.IsKeyPressed((int) KeyList.A))
				movement -= 300;
		
			if (Input.IsKeyPressed((int) KeyList.D))
				movement += 300;
		
			var kinematic1 = _player1.GetChild<KinematicBody2D>(0);
			kinematic1?.MoveAndCollide(new Vector2(movement * delta, 0));

			if (movement != 0)
				_player1.Walk();

		
			if (Input.IsKeyPressed((int) KeyList.O))
			{
				_player1.Attack(Attack.Punch);
			}
			if (Input.IsKeyPressed((int) KeyList.P))
			{
				_player1.Attack(Attack.Kick);
			}
			
			if (Input.IsKeyPressed((int) KeyList.Space))
			{
				_player1.Jump();
			}
		}
	}
}
