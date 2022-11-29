using Godot;
using System;

public class Ammo : Label
{
    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        Text = Game.player.currentAmmo + "/" + Game.player.maxAmmo;
    }
}
