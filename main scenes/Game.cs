using Godot;
using System;

public class Game : Node2D
{
	public static Node2D bulletSpace;
	public static Player player;
	public static Level level = null;
	private readonly string levelPath = "res://world/levels/";
	private int levelNumber = 0;

	public override void _Ready()
	{
		bulletSpace = GetNode<Node2D>("%BulletSpace");
		player = GetNode<Player>("%Player");

		NextLevel();
	} 

	public void NextLevel()
    {
		if (level != null)
			level.QueueFree();

		levelNumber++;	
		level = (Level)GD.Load<PackedScene>(levelPath + "Level" + levelNumber + ".tscn").Instance();
		GD.Print("Next level: " + levelNumber);
		level.Ready();
		CallDeferred("add_child", level);
		level.Connect("SignalNextLevel", this, nameof(NextLevel));
		level.Connect("SignalRestart", this, nameof(Restart));
	}

	public void Restart()
    {
		levelNumber--;
		NextLevel();
    }
}
