using Godot;
using System;

[Tool]
public class Door : AnimatedSprite
{
    private bool _isOpen;
    [Export] public bool IsOpen
    {
        get
        {
            return _isOpen;
        }
        set
        {
            if (value)
            {
                Animation = "Opening";
            }
            else
            {
                Animation = "Closing";
            }

            GetNode<CollisionShape2D>("StaticBody2D/CollisionShape2D").SetDeferred("disabled", value);
            _isOpen = value;
        }
    }

    [Export] string ID = "Door1";

    public override void _Ready()
    {
        //GetNode<ButtonListener>("ButtonListener").id = ID;
    }

    private void _on_Door_animation_finished()
    {
        switch(Animation)
        {
            case "Opening":
                {
                    Animation = "Open";
                    break;
                }
            case "Closing":
                {
                    Animation = "Closed";
                    break;
                }
        }
    }

    private void Open()
    {
        IsOpen = true;
    }
}
