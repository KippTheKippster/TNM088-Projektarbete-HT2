using Godot;
using System;

public abstract class Entity : KinematicBody2D //invisible object with characteristics one can use
{
	Vector2 velocity;
	const float gravity = 982F;
	
}

public override void _Proccess(float delta)
{
	MoveAndSlide(velocity);
	Test();	
}

public abstract void Test();
