using Godot;
using System;


public class Mouthman : Enemy
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	

	// Called when the node enters the scene tree for the first time.
	
	public override void _PhysicsProcess(float delta)
	{
		Move(delta);
	}
	
	private void Move(float delta) 
	{ 
		var player = (KinematicBody2D)GetNode("/root/Game/Player");
		var mouthman = (KinematicBody2D)GetNode("Mouthman");

		
		
		
		velocity.x = moveSpeed;

		MoveAndSlide(velocity, Vector2.Up);
	}


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
