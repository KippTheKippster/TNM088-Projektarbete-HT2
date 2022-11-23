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

    public override void OnHit()
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
<<<<<<< HEAD
            hitbox.AddToGroup("stompable");
=======
>>>>>>> c72ab8fbf5c6badc35a17b648d174175981739cd
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
