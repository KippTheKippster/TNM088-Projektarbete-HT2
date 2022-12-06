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

	public bool invincible;
	public bool active = true;
	[Export] public bool activateOnScreen = false;

	[Export] public string unlockId = "";

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite>("%Sprite");
		hitbox = GetNode<Area2D>("Hitbox");
		flashTimer = GetNode<Timer>("FlashTimer");
		Material = (Material)Material.Duplicate();
		
		if (activateOnScreen)
			active = false;
	}
	
	public override void _PhysicsProcess(float delta)
	{
		if (!active)
			return;

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

	public virtual void OnHit(int damage) 
	{
		HP -= damage;
		if (HP <= 0) 
		{ 
			Kill();
		}
		Flash();
		Game.audio.PlaySound("hitenemy.wav");
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
		Game.level.EmitSignal("SignalEnemyDied", unlockId);
	}

	public virtual void _on_Hitbox_area_entered(Area2D area)
	{
		EmitSignal(nameof(SignalOnHitboxEntered), area);
		if (area.GetParent().IsInGroup("PlayerBullet")) 
		{
			Bullet bullet = ((Bullet)area.GetParent());
			OnHit(bullet.Damage);
			bullet.Die();
		}
	}

	private void _on_VisibilityNotifier2D_screen_entered()
    {
		active = true;
		GD.Print("Is on screen!");
	}

	private void _on_DelayAdd_timeout()
    {
		GD.Print("ADDING!");
		Game.level.EmitSignal("SignalEnemyAdd", unlockId);
	}
}



