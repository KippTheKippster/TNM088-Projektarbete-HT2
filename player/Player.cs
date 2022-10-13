using Godot;
using System;

public class Player : Entity
{
	int inputX;
	int inputY;

	public override void _Process(float delta)
	{
		base._Process(delta);
		ReadInput(); 
		Move(delta);
	}

	private void ReadInput()
	{
		inputX = 0;
		inputY = 0;
		if (Input.IsActionPressed("right")) 
			inputX = 1;
		if (Input.IsActionPressed("left"))
			inputX = -1;
		if (Input.IsActionPressed("up"))
			Jump()
	}

	private void Move(float delta)
	{
		velocity = new Vector2(inputX * 100, velocity.y);
		velocity.y += gravity * delta;
	}
	
	public void Jump()
	{
		velocity = new Vector2(velocity.x,inputY * 100);
	}
	
	public override void Test()
	{
		GD.Print("Testing123");
	}
}
