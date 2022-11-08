using Godot;
using System;

public class Game : Node2D
{
	public static Node2D bulletSpace;

	public override void _Ready()
	{
		bulletSpace = GetNode<Node2D>("%BulletSpace");
	}
}
