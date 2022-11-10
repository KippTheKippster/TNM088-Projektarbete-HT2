using Godot;
using System;

[Tool]
public class Elevator : AnimatedSprite
{
    private bool _endElevator;
    [Export] public bool EndElevator
    {
        get
        {
            return _endElevator;
        }
        set
        {
            _endElevator = value;

            if (value)
                Close();
            else
                Open();
        }
    }

    public override void _Ready()
    {
        if (!_endElevator)
            Game.player.GlobalPosition = GlobalPosition;
    }

    public override void _Process(float delta)
    {
        if (!Engine.EditorHint)
        {
            //RotationDegrees += 180 * delta;
        }
    }

    public void Open()
    {
        Animation = "open";
    }
    public void Close()
    {
        Animation = "closed";
    }
}
