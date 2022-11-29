using Godot;
using System;

public class Flipper : Enemy
{
	bool flipped;

	public override void _Ready()
	{
		base._Ready();

		sprite.Animation = "default";
		sprite.Playing = true;
	}

	public override void _PhysicsProcess(float delta)
	{

	}

    public override void _on_Hitbox_area_entered(Area2D area)
    {
        EmitSignal(nameof(SignalOnHitboxEntered), area);
        if (area.GetParent().IsInGroup("PlayerBullet"))
        {
            if (flipped)
            {
                if (((Bullet)area.GetParent()).MoveVector.y > 0)
                {
                    Kill();
                }
            }

            OnHit(0);
            area.GetParent().QueueFree();
        }
    }

    public override void OnHit(int damage)
    {
        if (invincible)
        {

        }
        else
        {
            invincible = true;
            flipped = true;
            sprite.Animation = "flipping";
            AddToGroup("stompable");
        }
    }

	private void OnHitboxEntered(Area2D area)
	{
		//if (area.IsInGroup("FloorCheck"))
		{
			//if (flipped && ((Player)area.GetParent()).velocity.y > 0)
				//Kill();
		}
	}

	private void OnAnimationFinished()
	{
		string currentAnimation = sprite.Animation;

		switch (currentAnimation)
		{
			case "flipping":
			{
				sprite.Animation = "flipped";
				break;
			}
		}
	}
}
