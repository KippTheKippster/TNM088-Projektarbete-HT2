using Godot;
using System;

public class Circle : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Scale = new Vector2(0.01f, 0.01f);
    }

    public override void _Process(float delta)
    {
        Scale += 0.4f * delta * new Vector2(1, 1);
        Modulate -= new Color(0,0,0,1) * delta * 0.75f;
    }

    private void _on_Timer_timeout()
    {
        QueueFree();    
    }
}
