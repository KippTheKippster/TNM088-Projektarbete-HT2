using Godot;
using System;

[Tool]
public class TempPlatform : StaticBody2D
{
    private bool _on;
    [Export] public bool On
    {
        get
        {
            return _on;
        }
        set
        {
            _on = value;

            CollisionShape2D collision = GetNode<CollisionShape2D>("CollisionShape2D");

            AnimatedSprite sprite = GetNode<AnimatedSprite>("Sprite");

            if (value)
                sprite.Animation = "close";
            else
                sprite.Animation = "open";

            collision.Disabled = value;
        }
    }

    [Export] public float time = 1;

    public override void _Ready()
    {
        GetNode<Timer>("Timer").WaitTime = time;
    }

    private void _on_Timer_timeout()
    {
        On = !On;
    }
}
