using Godot;
using System;

[Tool]
public class Sparkbox : Node2D
{
    AnimationPlayer animation;
    private int _piece;
    [Export(PropertyHint.Range, "0,7,")] public int Piece
    {
        get
        {
            return _piece;
        }
        set
        {
            _piece = value;
            GetNode<AnimationPlayer>("AnimationPlayer").CurrentAnimation = value.ToString();
            Random random = new Random();
            int frame = random.Next(1, 7);
            GetNode<AnimatedSprite>("AnimatedSprite").Frame = frame;
        }
    }

    public override void _Ready()
    {
    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
