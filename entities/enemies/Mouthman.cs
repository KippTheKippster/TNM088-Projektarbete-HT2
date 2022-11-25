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
		float vMax = 200;

		totalVelocity = Mathf.Min(totalVelocity, vMax);

		moveSpeed = -4 ;
		Vector2 posDiff = playerPos - GlobalPosition;

		float xdiff = posDiff.x;
		float ydiff = posDiff.y;

		float airres = (1.0f - totalVelocity / vMax);

        if (GlobalPosition.x > playerPos.x)
		{
			velocity.x += xdiff;
        }
		else 
		{
            velocity.x += xdiff;
        }
        if (GlobalPosition.y > playerPos.y)
        {
            velocity.y += ydiff;
        }
        else
        {
            velocity.y += ydiff;
        }
		velocity.x *= airres;
		velocity.y *= airres;

		/*

		if (velocity.x > vMax) 
		{
			velocity.x = vMax;
		}
        if (velocity.y  > vMax)
        {
            velocity.y = vMax;
        }
        if (velocity.x < -vMax)
        {
            velocity.x = -vMax;
        }
        if (velocity.y < -vMax)
        {
            velocity.y = -vMax;
        }
		*/

        GD.Print(velocity.y);
        GD.Print(velocity.x);






        MoveAndSlide(velocity, Vector2.Up);
	}


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
