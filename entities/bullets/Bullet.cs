using Godot;
using System;

public class Bullet : Node2D
{
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

	public override void _Process(float delta)
	{
		Position += _moveVector * Speed * delta;
	}
}
