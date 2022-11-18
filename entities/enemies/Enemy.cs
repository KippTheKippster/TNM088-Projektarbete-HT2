using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public class Enemy : Entity
{
	internal AnimatedSprite sprite;

	[Signal] public delegate void SignalOnHitboxEntered(Area2D area);

	[Export] string[] signals = { "SignalEnemyDied" };

	public float moveSpeed = -50;
	
	public int HP = 1;

	public bool invincible;

	public override void _Ready()
	{
		GD.Print("ADDING!");
		Game.level.EmitSignal("SignalEnemyAdd");
		sprite = GetNode<AnimatedSprite>("%Sprite");
	}
	
	public override void _PhysicsProcess(float delta)
	{
		Move(delta);
	}
	
	private void Move(float delta) 
	{ 
		if (!IsOnFloor())
		{
			velocity.y += gravity * delta;
		}
		else if (velocity.y > 0)
		{
			velocity.y = 0;
		}

		velocity.x = moveSpeed;

		MoveAndSlide(velocity, Vector2.Up);
	}

	public virtual void OnHit() 
	{
		HP--;
		if (HP <= 0) 
		{ 
			Kill();
		}
	}

	public virtual void Kill() 
	{
		QueueFree(); 
		GD.Print("Enemy Died.");
		Game.level.EmitSignal("SignalEnemyDied");
	}

	private void _on_Hitbox_area_entered(Area2D area)
	{
		EmitSignal(nameof(SignalOnHitboxEntered), area);
		if (area.GetParent().IsInGroup("PlayerBullet")) 
		{
			OnHit();
			area.GetParent().QueueFree();
		}
	}
}



