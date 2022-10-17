using Godot;
using System;

public abstract class Entity : KinematicBody2D //invisible object with characteristics one can use
{
	public Vector2 velocity;
	public readonly float gravity = 982F;
}


