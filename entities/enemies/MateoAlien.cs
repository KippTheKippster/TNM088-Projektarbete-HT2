using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

[Tool]
public class MateoAlien : Enemy
{
    private bool _flipped;
    [Export] public bool Flipped
    {
        get
        {
            return _flipped;
        }
        set
        {
            _flipped = value;
            GetNode<AnimatedSprite>("%Sprite").FlipH = value;

            GD.Print("Mateo alien flipped is: " + value);

            if (value)
                moveSpeed = Mathf.Abs(moveSpeed) * -1;
            else
                moveSpeed = Mathf.Abs(moveSpeed);
        }
    }

    public override void _Ready()
    {
        Flipped = Flipped;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (!Engine.EditorHint)
        {
            base._PhysicsProcess(delta);
        }
    }
	
	private void OnRightCheckEntered(Area2D area)
	{
        Flipped = true;
	}
    
    private void OnLeftCheckEntered(Area2D area)
	{
		Flipped = false;
	}
}