using Godot;

public class Hud : Control
{
	private Player _player1;
	private Player _player2;
	
	private int _time;
	private float _currentTime;
	private bool _started;
	
	private Label _timeLabel;
	private Label _player1Name;
	private Label _player2Name;
	private ColorRect _life1Red;
	private ColorRect _life1Green;
	private ColorRect _life2Red;
	private ColorRect _life2Green;

	public override void _Ready()
	{
		_started = false;
		_timeLabel = GetNode<Label>("Time");

		_life1Red = GetNode<ColorRect>("Infos/Player1/LifeRed");
		_life1Green = GetNode<ColorRect>("Infos/Player1/LifeGreen");
		_player1Name = GetNode<Label>("Infos/Player1/Name");
		
		_life2Red = GetNode<ColorRect>("Infos/Player2/LifeRed");
		_life2Green = GetNode<ColorRect>("Infos/Player2/LifeGreen");
		_player2Name = GetNode<Label>("Infos/Player2/Name");
	}

	public void StartBattle(Player player1, Player player2, int time)
	{
		_time = time;

		_player1 = player1;
		_player1.CurrentLife = _player1.FullLife;
		_player1Name.Text = player1.Description;

		_player2 = player2;
		_player2.CurrentLife = _player2.FullLife;
		_player2Name.Text = player2.Description;
		
		_started = true;
		_currentTime = 0f;
	}

	public override void _Process(float delta)
	{
		if (!_started)
			return;

		var redSize1 = _life1Red.RectSize;
		var percent1 = (float) _player1.CurrentLife / _player1.FullLife;
		_life1Green.RectSize = new Vector2(redSize1.x * percent1, redSize1.y);
		
		var redSize2 = _life2Red.RectSize;
		var percent2 = (float) _player2.CurrentLife / _player2.FullLife;
		_life2Green.RectSize = new Vector2(redSize2.x * percent2, redSize2.y);
		_life2Green.RectPosition = new Vector2(
			_life2Red.RectPosition.x + redSize2.x - _life2Green.RectSize.x,
			_life2Green.RectPosition.y);

		_currentTime += delta;
		var remaining = _time - _currentTime;
		_timeLabel.Text = remaining.ToString("0");
	}
}
