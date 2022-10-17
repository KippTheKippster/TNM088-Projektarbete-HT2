using Godot;
using System;

public class Player : Entity
{
	[Export] readonly float speedX = 200f;
	[Export] readonly float jumpStrength = 600f;

	int inputX;

    public override void _PhysicsProcess(float delta)
    {
		ReadInput();
		Move(delta);
	}

	private void ReadInput()
	{
		inputX = 0;

		if (Input.IsActionPressed("right")) 
			inputX = 1;
		if (Input.IsActionPressed("left"))
			inputX = -1;
		if (Input.IsActionJustPressed("up"))
			if (IsOnFloor())
				Jump();
	}

	private void Move(float delta)
	{
		velocity.x = inputX * speedX;

		if (!IsOnFloor())
        {
			velocity.y += gravity * delta;
		}
		else if (velocity.y >= 0)
        {
			velocity.y = 0;
		}

		MoveAndSlide(velocity, Vector2.Up);
	}
	
	private void Jump()
	{
		velocity.y = -jumpStrength;
	}
}
