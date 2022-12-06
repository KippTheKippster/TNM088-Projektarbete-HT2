using Godot;
using System;

[Tool]
public class Turret : Node2D
{
    AnimatedSprite sprite;
    AnimatedSprite chargeSprite;
    Gun gun;
    Timer chargeTimer;
    Timer shootTimer;

    bool _flipped;
    [Export] bool Flipped
    {
        get
        {
            return _flipped;
        }
        set
        {
            _flipped = value;

            if (Flipped)
            {
                Scale = new Vector2(1, 1);
            }
            else
            {
                Scale = new Vector2(-1, 1);
            }
        }
    }

    public override void _Ready()
    {
        sprite = GetNode("AnimatedSprite") as AnimatedSprite;
        chargeSprite = GetNode("Charge") as AnimatedSprite;
        gun = GetNode("Gun") as Gun;
        chargeTimer = GetNode("StartChargeTimer") as Timer;
        shootTimer = GetNode("ShootTimer") as Timer;

        sprite.Animation = "default";
    }
    
    private void _on_AnimatedSprite_animation_finished()
    {
        if (sprite.Animation == "shoot")
            sprite.Animation = "default";
    }

    private void _on_ShooTimer_timeout()
    {
        if (Engine.EditorHint)
            return;

        gun.Shoot(gun.GlobalPosition, new Vector2(-Scale.x, 0));
        GD.Print("Shoot...");
        chargeTimer.Start();
        shootTimer.Start();
        chargeSprite.Visible = false;
        sprite.Animation = "shoot"; 
    }

    private void _on_StartChargeTimer_timeout()
    {
        if (Engine.EditorHint)
            return;

        chargeSprite.Visible = true;
        GD.Print("Charging...");
    }
}
