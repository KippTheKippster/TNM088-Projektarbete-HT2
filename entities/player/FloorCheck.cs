using Godot;
using System;

public class FloorCheck : Area2D
{
    [Signal] public delegate void SignalHitGround();

    private Timer delay;
    private int floorCounter = 0;
    private bool valid;

    public bool IsOnFloor
    {
        get
        {
            if (valid)
                return true;
            else
                return false;
        }
    }

    public override void _Ready()
    {
        delay = GetNode<Timer>("DelayTimer");
    }

    private void OnFloorCheckEntered(Node body)
	{
        floorCounter++;
        valid = true;
        delay.Stop();
        GD.Print("Floor entered: " + body.Name + " : " + floorCounter);

        if (floorCounter == 1)
        {
            EmitSignal(nameof(SignalHitGround));
        }
    }

    private void OnFloorCheckExisted(Node body)
	{
		floorCounter--;
        GD.Print("Floor existed: " + body.Name + " : " + floorCounter);

        if (floorCounter <= 0)
        {
            delay.Start();
        }
	}

    private void OnDelayTimeout()
    {
        valid = false;
        GD.Print("CANT JUMP NOW");
    }
}
