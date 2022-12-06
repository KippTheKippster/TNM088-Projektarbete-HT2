using Godot;
using System;

public class KillListener : Listener
{
    public override void _Ready()   
    {
        base._Ready();
        Game.level.Connect("SignalEnemyAdd", this, nameof(OnAdd));
        Game.level.Connect("SignalEnemyDied", this, nameof(OnActivate));
    }
}
