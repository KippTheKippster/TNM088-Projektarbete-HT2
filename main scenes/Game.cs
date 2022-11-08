using Godot;
using System;

public class Game : Node2D
{
	public static Node2D bulletSpace;
	public static Player player;
	public static Level level;
	string levelPath = "res://world/levels/";
	public int levelNumber = 1;

	public override void _Ready()
	{
		bulletSpace = GetNode<Node2D>("%BulletSpace");
		player = GetNode<Player>("%Player");

		level = (Level)GD.Load<PackedScene>(levelPath + "Level" + levelNumber + ".tscn").Instance() ;
		AddChild(level);
	}
}
