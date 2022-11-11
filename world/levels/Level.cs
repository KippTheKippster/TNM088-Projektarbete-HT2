using Godot;
using System;

public class Level : Node2D
{
	[Signal] public delegate void SignalEnemyDied();

	public Elevator startElevator;
	public Elevator endElevator;

	public override void _Ready()
	{
		startElevator = GetNode<Elevator>("%StartElevator");
		endElevator = GetNode<Elevator>("%EndElevator");

		Connect(nameof(SignalEnemyDied), this, nameof(OnEnemyDied));
	}

	public void Open()
	{
		endElevator.Open();
	}

	private void OnEnemyDied()
    {
		GD.Print("bruh");
    }
}
