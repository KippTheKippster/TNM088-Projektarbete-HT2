using Godot;
using System;

public class Game : Node2D
{
	public static Node2D bulletSpace;
	public static Player player;
	public static Camera2D camera;
	public static Level level = null;
	public static Audio audio;
	public static Time time;
	private readonly string levelPath = "res://world/levels/";
	[Export(PropertyHint.Range, "1,1000,")] public int levelNumber = 1;

	[Signal] public delegate void SignalPlaySound(string path);

	bool hasWon = false;

	public override void _Ready()
	{
		bulletSpace = GetNode<Node2D>("%BulletSpace");
		player = GetNode<Player>("%Player");
		camera = player.GetNode<Camera2D>("Camera");
		audio = GetNode<Audio>("Audio");
		time = GetNode<Time>("Ui/Time");

		NextLevel();
	} 

	public override void _PhysicsProcess(float delta)
	{
		if (Input.IsActionJustPressed("fullscreen"))
		{
			OS.WindowFullscreen = !OS.WindowFullscreen;
		}
	}

	public void NextLevel()
	{
		if (level != null)
			level.QueueFree();

		string path = levelPath + "Level" + levelNumber + ".tscn";

		if (!ResourceLoader.Exists(path))
		{
			if (!hasWon)
				Win();
			else
				GetTree().Quit();

			return;
		}

		level = (Level)GD.Load<PackedScene>(path).Instance();
		GD.Print("Next level: " + levelNumber);
		level.Ready();
		CallDeferred("add_child", level);
		level.Connect("SignalNextLevel", this, nameof(NextLevel));
		level.Connect("SignalRestart", this, nameof(Restart));
		levelNumber++;
	}

	public void Win()
	{
		hasWon = true;
		level = (Level)GD.Load<PackedScene>("res://world/levels/LevelWin.tscn").Instance();
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
