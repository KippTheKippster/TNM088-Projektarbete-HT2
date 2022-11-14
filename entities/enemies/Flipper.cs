using Godot;
using System;

public class Flipper : Enemy
{
    bool invincible;

    public override void _Ready()
    {
        base._Ready();

        sprite.Animation = "default";
        sprite.Playing = true;
    }

    public override void _PhysicsProcess(float delta)
    {

    }

    public override void OnHit()
    {
        if (invincible)
        {

        }
        else
        {
            invincible = true;
            sprite.Animation = "flipping";
        }
    }

    private void OnHitboxEntered(Area2D area)
    {
        if (area.IsInGroup("FloorCheck"))
            if (invincible)
                Kill();
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
