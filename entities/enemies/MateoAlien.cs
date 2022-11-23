using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

[Tool]
public class MateoAlien : Enemy
{
    private bool _flipped;
    [Export] public bool Flipped
    {
        get
        {
            return _flipped;
        }
        set
        {
            _flipped = value;
            GetNode<AnimatedSprite>("%Sprite").FlipH = value;

            if (value)
                moveSpeed = Mathf.Abs(moveSpeed) * -1;
            else
                moveSpeed = Mathf.Abs(moveSpeed);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (!Engine.EditorHint)
        {
            base._PhysicsProcess(delta);
        }
    }

	private void _on_Hitbox_area_entered(Area2D area)
	{
		EmitSignal(nameof(SignalOnHitboxEntered), area);
		if (area.GetParent().IsInGroup("PlayerBullet")) 
		{
			OnHit();
			area.GetParent().QueueFree();
		}
	}
	
	private void OnRightCheckEntered(Area2D area)
	{
        Flipped = true;
	}
    
    private void OnLeftCheckEntered(Area2D area)
	{
		Flipped = false;
	}
}



