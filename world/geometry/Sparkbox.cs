using Godot;
using System;

[Tool]
public class Sparkbox : Node2D
{
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
            GetNode<AnimatedSprite>("AnimatedSprite").Frame = 0;
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
