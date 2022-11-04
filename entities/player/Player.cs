using Godot;
using System;

public class Player : Entity
{
	FloorCheck floorCheck;
	PlayerGun gun;

	[Export] readonly float speedX = 200f;
	[Export] readonly float jumpStrength = 600f;

	int inputX;
	int aimVerticalDirection;

    public override void _Ready()
    {
        floorCheck = GetNode<FloorCheck>("%FloorCheck");
		gun = GetNode<PlayerGun>("%Gun");
    }

    public override void _PhysicsProcess(float delta)
    {
		ReadInput();
		Move(delta);
		Gun();
	}

	private void ReadInput()
	{
		inputX = 0;

		if (Input.IsActionPressed("right")) 
			inputX = 1;
		if (Input.IsActionPressed("left"))
			inputX = -1;
		if (Input.IsActionJustPressed("jump"))
			if (floorCheck.IsOnFloor)
				Jump();

		if (Input.IsActionPressed("up"))
			aimVerticalDirection = -1;
		else if (Input.IsActionPressed("down"))
			aimVerticalDirection = 1;
		else
			aimVerticalDirection = 0;	
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

	private void Gun()
	{
		gun.Rotation = (Mathf.Pi / 2f) * aimVerticalDirection;
	}
}
