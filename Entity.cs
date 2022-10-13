using Godot;
using System;

public abstract class Entity : KinematicBody2D //invisible object with characteristics one can use
{
	public Vector2 velocity;
	public const float gravity = 982F;
	
	public override void _Process(float delta) //overwrite (viktigare i jerakin)
{
	MoveAndSlide(velocity);
	Test();	
}

public abstract void Test();
}


