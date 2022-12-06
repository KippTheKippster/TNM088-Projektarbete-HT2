using Godot;
using System;

public class ButtonListener : Listener
{
    public override void _Ready()
    {
        base._Ready();
        Game.level.Connect("SignalButtonPressed", this, nameof(OnActivate));
        Game.level.Connect("SignalButtonAdd", this, nameof(OnAdd));
    }
}
