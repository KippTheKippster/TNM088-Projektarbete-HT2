using Godot;
using System;

public class FloorCheck : Area2D
{
    private int floorCounter = 0;

    public bool IsOnFloor
    {
        get
        {
            if (floorCounter > 0)
                return true;
            else
                return false;
        }
    }

	private void OnFloorCheckEntered(Node body)
	{
        floorCounter++;
	}

    private void OnFloorCheckExisted(Node body)
	{
		floorCounter--;   
	}
}
