using Godot;
using System;



public class Mouthman : Enemy
{

	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// public float moveSpeed = -10;
	

	// Called when the node enters the scene tree for the first time.
	
	public override void _PhysicsProcess(float delta)
	{
		Move(delta);
	}
	
	private void Move(float delta) 
	{ 
		Vector2 playerPos = Game.player.GlobalPosition;
		

		float totalVelocity = (float)Math.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
		// normal hastighet ar typ 250
		

		moveSpeed = 350;
		float maxSpeed = 5000;

		float airRes =  1 -(totalVelocity / maxSpeed);

        // ny losning

        // anvand posDiff for att fa fram en vektor som pekar pa spelaren

        Vector2 posDiff = playerPos - GlobalPosition;
		posDiff.y += -6;

		// normalisera vektorn

		posDiff = posDiff.Normalized();

        // multiplicera med moveSpeed

        velocity *= airRes;
        velocity += posDiff * delta * moveSpeed;
		

		if (IsOnFloor() || IsOnCeiling())
			velocity.y *= 0.01f;
        if (IsOnWall())
            velocity.x *= 0.01f;
		
		if (GlobalPosition.x > playerPos.x) 
		{
            GlobalScale = new Vector2(-1, 1);
			GlobalRotation = 0;
        }
        else
        {
            GlobalScale = new Vector2( 1, 1);
			GlobalRotation = 0;
        }

        MoveAndSlide(velocity, Vector2.Up);
	}


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
