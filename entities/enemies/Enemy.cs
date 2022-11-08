using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public class Enemy : Entity
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	public int HP = 1;
	float moveSpeed = -50;
	

	// Called when the node enters the scene tree for the first time.
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
	}

	private void _on_Hitbox_area_entered(Area2D area)
	{
		if (area.GetParent().IsInGroup("PlayerBullet")) 
		{
			OnHit();
		}
		
	}


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}



