using Godot;
using System;

public class Game : Node2D
{
	public static Node2D bulletSpace;
	public static Player player;
	public static Camera2D camera;
	public static Level level = null;
	private readonly string levelPath = "res://world/levels/";
	[Export(PropertyHint.Range, "0,100,")] public int levelNumber = 1;

	public override void _Ready()
	{
		bulletSpace = GetNode<Node2D>("%BulletSpace");
		player = GetNode<Player>("%Player");
		camera = player.GetNode<Camera2D>("Camera");

		NextLevel();
	} 

	public void NextLevel()
	{
		if (level != null)
			level.QueueFree();

		level = (Level)GD.Load<PackedScene>(levelPath + "Level" + levelNumber + ".tscn").Instance();
		GD.Print("Next level: " + levelNumber);
		level.Ready();
		CallDeferred("add_child", level);
		level.Connect("SignalNextLevel", this, nameof(NextLevel));
		level.Connect("SignalRestart", this, nameof(Restart));
		levelNumber++;
	}

	public void Restart()
    {
		levelNumber--;
		NextLevel();
    }
}
