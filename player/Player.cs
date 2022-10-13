using Godot;
using System;

public class Player : KinematicBody2D
{
    Vector2 velocity;

    const float gravity = 982F;
    
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

        MoveAndSlide(velocity, Vector2.Up);
    }
}
