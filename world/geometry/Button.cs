using Godot;
using System;

[Tool]
public class Button : AnimatedSprite
{
    private bool _pressed;
    public bool Pressed
    {
        get
        {
            return _pressed;
        }
        set
        {
            if (value && _pressed != value)
            {
                Animation = "Pressed";
                Game.level.EmitSignal("SignalButtonPressed", unlockID);
            }
            else if (!value)
                Animation = "NotPressed";

            _pressed = value;
        }
    }

    [Export] public string unlockID = "EndElevator";

    private void _on_DelayAdd_timeout()
    {
        GD.Print("ButtonEmiting");
        Game.level.EmitSignal("SignalButtonAdd", unlockID);
        GetNode<Timer>("DelayAdd").QueueFree();
    }

    private void _on_Area2D_body_entered(Node body)
    {
        if (body.IsInGroup("Player"))
        {
            Pressed = true;
        }
    }
}
