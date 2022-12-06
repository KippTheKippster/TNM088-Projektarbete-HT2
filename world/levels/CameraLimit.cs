using Godot;
using System;

public class CameraLimit : Sprite
{
    Vector2 totalSize;

    public override void _Ready()
    {
        totalSize = Texture.GetSize() * Scale;
        totalSize = new Vector2(Mathf.Max(totalSize.x, 384), Mathf.Max(totalSize.y, 216));

        Game.camera.LimitLeft = (int)GlobalPosition.x;
        Game.camera.LimitTop = (int)GlobalPosition.y;
        Game.camera.LimitRight = (int)GlobalPosition.x + (int)totalSize.x;
        Game.camera.LimitBottom = (int)GlobalPosition.y + (int)totalSize.y;

        Visible = false;
    }

}
