using Godot;
using System;

public class Bullet : Node2D
{
	public AnimatedSprite Sprite { get; set; }
	private float _angle = 0;
	public float Angle
	{
		get
		{
			return _angle;
		}
		set
		{
			_angle = value;
			_moveVector = new Vector2(Mathf.Cos(value), Mathf.Sin(value));  
			if (RotateSprite)
				Rotation = value;
		}
	}

	public float Speed { get; set; } = 100f;
	public int Damage { get; set; } = 1;	
	public bool RotateSprite;

	private Vector2 _moveVector;
	public Vector2 MoveVector
	{
		get
		{
			return _moveVector;
		}
		set
		{
			_moveVector = value.Normalized();
			_angle = Mathf.Atan2(_moveVector.y, _moveVector.x);
			if (RotateSprite)
				Rotation = _angle;
		}
	}

    public override void _Ready()
    {
		Sprite = GetNode<AnimatedSprite>("Sprite");
		Sprite.Animation = "default";
	}

    public override void _Process(float delta)
	{
		Position += _moveVector * Speed * delta;
	}


	private void _on_Sprite_animation_finished()
    {
		if (Sprite.Animation == "default")
			Sprite.Animation = "idle";
		else if (Sprite.Animation == "death")
			QueueFree();
    }

	public void Die()
    {
		Sprite.Animation = "death";
		Sprite.FlipH = true;
		Sprite.FlipV = true;
		Speed = 0;
	}

	private void _on_Area2D_body_entered(Node body)
    {
		Die();
    }

	private void _on_DeathTimer_timeout()
    {
		Die();
    }
}
