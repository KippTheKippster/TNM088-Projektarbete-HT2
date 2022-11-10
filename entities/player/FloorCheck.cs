using Godot;
using System;

public class FloorCheck : Area2D
{
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
    }

    private void OnFloorCheckExisted(Node body)
	{
		floorCounter--;  
        
        if (floorCounter <= 0)
        {
            delay.Start();
        }
	}

    private void OnDelayTimeout()
    {
        valid = false;
    }
}
