using Godot;
using System;

public class Model : AnimatedSprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    private void _on_Model_animation_finished()
    {
        if (Animation == "InAirStart")
        {
            Animation = "InAir";
        }
        else if (Animation == "JumpStart")
        {
            Animation = "Jump";
        }
        else if (Animation == "PushBackStart")
        {
            Animation = "Pushback";
        }
    }
}
