using Godot;
using System;

public class Player : Entity
{
	int inputX;

	public override void _Process(float delta)
	{
	   ReadInput(); 
	   Move(delta);
	}

	private void ReadInput()
	{
		inputX = 0;
		if (Input.IsActionPressed("right")) 
			inputX = 1;
		if (Input.IsActionPressed("left"))
			inputX = -1;
	}

	private void Move(float delta)
	{
		velocity = new Vector2(inputX * 100, velocity.y);
		velocity.y += gravity * delta;
	}
	
	public override void Test()
	{
		GD.Print("Testing123");
	}
}
