using Godot;

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

	private bool Initialized => _tower != null;

	private Tower _tower;
	private bool _facingLeft;
	private bool _attacking;

	public void Initialize(Node playable)
	{
		AddChild(playable);
		_tower = (Tower) playable;
		_tower.Anim.Play("idle");
		_tower.Anim.Connect("animation_finished", this, nameof(AnimationFinished));
	}
	
	public override void _Ready()
	{
	}
	
	public void Walk()
	{
		if (Initialized || _attacking) return;
		_tower.Anim.Play("walk");
	}
	
	public void Jump()
	{
		if (Initialized || _attacking) return;
		_tower.Anim.Play("jump");
	}

	public void Attack(Attacks attack)
	{
		if (Initialized || _attacking) return;
		_attacking = true;
		_tower.Anim.Stop();
		_tower.Anim.Play(attack.ToString().ToLower());
	}

	public void AnimationFinished(string name)
	{
		_attacking = false;
		_tower.Anim.Play("idle");
	}
}
