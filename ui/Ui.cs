using Godot;
using System;

public class Ui : CanvasLayer
{
    public static Dialogue dialogue;

    public override void _Ready()
    {
        dialogue = GetNode<Dialogue>("%Dialogue");
    }
}
