using Godot;
using System;

public class Player : Entity
{
	FloorCheck floorCheck;
	PlayerGun gun;
	Timer jumpTimer;
	Vector2 moveSpeed;
	Vector2 targetMoveSpeed;
	Vector2 externalSpeed;

	[Export] readonly float speedX = 100f;
	[Export] readonly float jumpStrength = 300f;
	[Export] readonly float moveAcceleration = 6f;
	[Export] readonly float groundDeceleration = 6f;
	[Export] readonly float airDeceleration = 6f;
	[Export] readonly float gunKnockback = 200f;
	[Export] readonly float jumpGravityScale = 0.3f;
	[Export] readonly float holdDownTime = 0.2f;

	float gravityScale = 1.0f;

	int inputX;
	int directionX = 1;
	int directionY;

	bool isJumping;
	public bool active = true;

	public override void _Ready()
	{
		floorCheck = GetNode<FloorCheck>("%FloorCheck");
		gun = GetNode<PlayerGun>("%PlayerGun");
		jumpTimer = GetNode<Timer>("JumpTimer");
		jumpTimer.WaitTime = holdDownTime;
	}

	public override void _PhysicsProcess(float delta)
	{
		if (!active)
			return;

		ReadInput();
		Jump();
		Move(delta);
	}

	private void ReadInput()
	{
		inputX = 0;

		if (Input.IsActionPressed("right")) 
			inputX = 1;
		if (Input.IsActionPressed("left"))
			inputX = -1;

		if (Input.IsActionPressed("up"))
			directionY = -1;
		else if (Input.IsActionPressed("down"))
			directionY = 1;
		else
			directionY = 0;

		Gun();

		if (Input.IsActionJustPressed("shoot"))
			Shoot();
	}

	private void Move(float delta)
	{
		float weight;

		if (inputX != 0)
		{
			directionX = inputX;
			GlobalScale = new Vector2(directionX, 1);
			GlobalRotation = 0;
		}

		if (IsOnFloor())
		{
			weight = groundDeceleration;
		}
		else
		{
			weight = airDeceleration;
		}

		targetMoveSpeed.x = inputX * speedX;
		moveSpeed.x = Mathf.Lerp(moveSpeed.x, targetMoveSpeed.x, moveAcceleration * delta);
		externalSpeed.x = Mathf.Lerp(externalSpeed.x, 0, weight * delta);

		if (!IsOnFloor())
		{
			externalSpeed.y += gravity * gravityScale * delta;
		}
		else if (externalSpeed.y > 0)
		{
			externalSpeed.y = 0;
		}

		velocity = moveSpeed + externalSpeed;

		MoveAndSlide(velocity, Vector2.Up);
	}
	
	private void Jump()
	{
		if (Input.IsActionJustPressed("jump"))
		{
			gravityScale = jumpGravityScale;
			if (floorCheck.IsOnFloor)
            {
                isJumping = true;
                jumpTimer.Start();
            }
        }
		else if (Input.IsActionJustReleased("jump"))
        {
            isJumping = false;
			gravityScale = 1.0f;
        }

        if (isJumping)
			externalSpeed.y = -jumpStrength;
	}

	public void OnJumpTimerTimeout()
	{
        isJumping = false;
	}

	private void Gun()
	{
		gun.Rotation = (Mathf.Pi / 2f) * directionY;
	}

	private void Shoot()
	{
		Vector2 shootVector;

		if (directionY != 0)
			shootVector = new Vector2(0, directionY);
		else
			shootVector = new Vector2(directionX, 0);

		externalSpeed = -shootVector * gunKnockback;

		gun.Shoot(GlobalPosition, shootVector);
	}
}
