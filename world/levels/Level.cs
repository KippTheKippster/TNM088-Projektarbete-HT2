using Godot;
using System;

public class Level : Node2D
{
	public Elevator startElevator;
	public Elevator endElevator;

	public override void _Ready()
	{
		startElevator = GetNode<Elevator>("%StartElevator");
		endElevator = GetNode<Elevator>("%EndElevator");
	}

	public void Open()
	{
		endElevator.Open();
	}
}
