using Godot;
using System;

public class Player : Entity
{
	FloorCheck floorCheck;
	public PlayerGun gun;
	Gun chargeGun;
	Timer jumpTimer;
	Timer chargeTimer;
	Timer reloadTimer;
	Timer deathTimer;
	AnimatedSprite chargeEffect;
	public AnimatedSprite model;

	public Vector2 moveSpeed;
	public Vector2 targetMoveSpeed;
	public Vector2 externalSpeed;

	[Export] readonly float speedX = 100f;
	[Export] readonly float jumpStrength = 300f;
	[Export] readonly float moveAcceleration = 6f;
	[Export] readonly float groundDeceleration = 6f;
	[Export] readonly float airDeceleration = 6f;
	[Export] readonly float gunKnockback = 200f;
	[Export] readonly float chargeKnockback = 350f;
	[Export] readonly float jumpGravityScale = 0.3f;
	[Export] readonly float holdDownTime = 0.2f;
    [Export] public readonly int maxAmmo = 3;

	float gravityScale = 1.0f;

	int inputX;
	int directionX = 1;
	int directionY;
	public int currentAmmo;

	bool isJumping;
	bool isCharged;
	public bool active = true;

	public override void _Ready()
	{
		floorCheck = GetNode<FloorCheck>("%FloorCheck");
		gun = GetNode<PlayerGun>("%PlayerGun");
		chargeGun = GetNode<Gun>("%ChargeGun");
		jumpTimer = GetNode<Timer>("JumpTimer");
		chargeTimer = GetNode<Timer>("%ChargeTimer");
		reloadTimer = GetNode<Timer>("%ReloadTimer");
		deathTimer = GetNode<Timer>("%DeathTimer");
		chargeEffect = GetNode<AnimatedSprite>("%ChargeEffect");
		model = GetNode<AnimatedSprite>("%Model");

		floorCheck.Connect("SignalHitGround", this, "Reload");

		currentAmmo = maxAmmo;
		jumpTimer.WaitTime = holdDownTime;
	}

	public override void _PhysicsProcess(float delta)
	{
		if (!active)
			return;

		ReadInput();
		Jump();	
		Gun();
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
	}

	private void Move(float delta)
	{
		float weight;

		if (inputX != 0)
		{
			directionX = inputX;
			GlobalScale = new Vector2(directionX, 1);
			GlobalRotation = 0;
			if (IsOnFloor())
				model.Animation = "Run"; 
		}
		else
		{
			if (IsOnFloor())
				model.Animation = "Idle";	
        }

		if (IsOnFloor())
		{
			weight = groundDeceleration;
		}
		else
		{
			weight = airDeceleration;
			if (model.Animation != "Pushback")
				model.Animation = "InAir";
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

	private void StartChargin()
    {
		chargeTimer.Start();
		chargeEffect.Visible = true;
		chargeEffect.Animation = "Half";
	}

	private void Gun()
	{
		gun.Rotation = (Mathf.Pi / 2f) * directionY;

		if (Input.IsActionJustPressed("shoot") && currentAmmo > 0)
        {
			Shoot();
			StartChargin();

			if (currentAmmo >= 0 && floorCheck.IsOnFloor)
				reloadTimer.Start();
		}
		else if (Input.IsActionJustReleased("shoot"))
        {
			if (isCharged)
            {
				GD.Print("Charge shot");
				ChargeShot();
            }

			StopCharge();
		}
	}

	public void StopCharge()
	{
		chargeTimer.Stop();
		isCharged = false;
		chargeEffect.Visible = false;
	}

	private void OnChargeTimerTimeout()
    {
		isCharged = true;
		chargeEffect.Animation = "Full";
		GD.Print("Charge Shot Ready");
    }

	private void Shoot()
	{
		Vector2 shootVector;

		if (directionY != 0)
			shootVector = new Vector2(0, directionY);
		else
			shootVector = new Vector2(directionX, 0);

		if (shootVector.y > 0)
			externalSpeed.y = -gunKnockback;

		gun.Shoot(GlobalPosition, shootVector);
		currentAmmo--;
	}

	private void ChargeShot()
    {
		Vector2 shootVector;

		if (directionY != 0)
			shootVector = new Vector2(0, directionY);
		else
			shootVector = new Vector2(directionX, 0);

		externalSpeed = -shootVector * chargeKnockback;

		chargeGun.Shoot(GlobalPosition, shootVector);
		model.Animation = "Pushback";
		currentAmmo = 0;

		if (floorCheck.IsOnFloor)
			reloadTimer.Start();
		else
			model.Animation = "Pushback";
	}

	private void Reload()
    {
		currentAmmo = maxAmmo;
		reloadTimer.Stop();
		if (Input.IsActionPressed("shoot") && !isCharged)
			StartChargin();
	}

	public void Kill(PlayerDeath death = PlayerDeath.Default)
    {
		active = false;
		//Visible = false;
		gun.Visible = false;

		switch (death)
		{
			case PlayerDeath.Default:
				{
					model.Animation = "DeathDefault";
					break;
				}
			case PlayerDeath.Electricity:
                {
					model.Animation = "DeathElectrified";
					break;
                }
		}
	}

	private void _on_Model_animation_finished()
    {
		if (model.Animation.Contains("Death"))
        {
			deathTimer.Start();
			GD.Print("Finnished");

		}
    }

	private void _on_DeathTimer_timeout()
	{
		Game.level.EmitSignal("SignalRestart");
	}
}

public enum PlayerDeath
{
	Default,
	Electricity
}
