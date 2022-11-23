using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public class Enemy : Entity
{
	internal AnimatedSprite sprite;
	internal Area2D hitbox;
	internal Timer flashTimer;

	[Signal] public delegate void SignalOnHitboxEntered(Area2D area);

	[Export] public float moveSpeed = -50;
	
	[Export] public int HP = 2;

	[Export] string[] signals = { "SignalEnemyDied" };


	public bool invincible;

	public override void _Ready()
	{
		GD.Print("ADDING!");
		Game.level.EmitSignal("SignalEnemyAdd");
		sprite = GetNode<AnimatedSprite>("%Sprite");
		hitbox = GetNode<Area2D>("%Hitbox");
		flashTimer = GetNode<Timer>("FlashTimer");
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
		Flash();
	}

	public void Flash()
    {
		(Material as ShaderMaterial).SetShaderParam("flash_shift", 1.0);
		flashTimer.Start();
	}

	private void OnFlashTimeout()
    {
		(Material as ShaderMaterial).SetShaderParam("flash_shift", 0.0);
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



