using Godot;
using System;

public class WinMenu : Control
{
    Node2D scrollText;
    [Export] float scrollSpeed = 20f;
    public override void _Ready()
    {
        scrollText = GetNode<Node2D>("ScrollText");
        Game.time.active = false;
        GetNode<Label>("%Time").Text += Game.time.Text;
    }

    public override void _Process(float delta)
    {
        scrollText.GlobalPosition += new Vector2(0, -1) * scrollSpeed * delta;
    }
}
