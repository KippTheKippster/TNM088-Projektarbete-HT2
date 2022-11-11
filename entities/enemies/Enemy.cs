using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public class Enemy : Entity
{
	[Export] string[] signals = { "SignalEnemyDied" };

	public float moveSpeed = -50;
	
	public int HP = 1;

	public override void _Ready()
	{
		
	}
	
	public override void _PhysicsProcess(float delta)
	{
		Move(delta);
	}
	
	void Move(float delta) 
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

	void OnHit() 
	{
		HP--;
		if (HP <= 0) 
		{ 
			Death();
		}
	}

	void Death() 
	{
		QueueFree(); 
		GD.Print("Enemy Died.");
		for (int i = 0; i < signals.Length; i++)
        {
            Game.level.EmitSignal(signals[i]);
		}
	}

	private void _on_Hitbox_area_entered(Area2D area)
	{
		if (area.GetParent().IsInGroup("PlayerBullet")) 
		{
			OnHit();
		}	
	}
}



