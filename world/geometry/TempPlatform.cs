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

            Sprite on = GetNode<Sprite>("SpriteOn");
            Sprite off = GetNode<Sprite>("SpriteOff");
            CollisionShape2D collision = GetNode<CollisionShape2D>("CollisionShape2D");

            on.Visible = !value;
            off.Visible = value;

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
