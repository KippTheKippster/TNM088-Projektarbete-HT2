using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

[Tool]
public class MateoAlien : Enemy
{
<<<<<<< HEAD
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

			if (value)
				moveSpeed = Mathf.Abs(moveSpeed) * -1;
			else
				moveSpeed = Mathf.Abs(moveSpeed);
		}
	}
=======
    int floorRightCounter;
    int floorLeftCounter;

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
>>>>>>> 0f6657e36398a6ef28d38903caec7f3a56124152

    [Export] public bool stayOnLedge = true;

    public override void _Ready()
    {
        base._Ready();
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
<<<<<<< HEAD
		Flipped = true;
	}
	
	private void OnLeftCheckEntered(Area2D area)
=======
        Flipped = true;
        GD.Print("MateoAlien right!");

    }
    
    private void OnLeftCheckEntered(Area2D area)
>>>>>>> 0f6657e36398a6ef28d38903caec7f3a56124152
	{
		Flipped = false;
        GD.Print("MateoAlien left!");
	}

    private void OnFloorRightCheck(Area2D area)
    {
        floorRightCounter++;
    }

    private void OnFloorLeftCheck(Area2D area)
    {
        floorLeftCounter++;
    }

    private void FloorRightExit(Area2D area)
    {
        floorRightCounter--;

        if (floorRightCounter <= 0 && stayOnLedge)
            Flipped = true;
    }

    private void FloorLeftExit(Area2D area)
    {
        floorLeftCounter--;

        if (floorLeftCounter <= 0 && stayOnLedge)
            Flipped = false;
    }
}