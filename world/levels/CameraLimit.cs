using Godot;
using System;

public class CameraLimit : Sprite
{
    Vector2 totalSize;

    public override void _Ready()
    {
        totalSize = Texture.GetSize() * Scale;

        Game.camera.LimitLeft = (int)GlobalPosition.x;
        Game.camera.LimitTop = (int)GlobalPosition.y;
        Game.camera.LimitRight = (int)GlobalPosition.x + (int)totalSize.x;
        Game.camera.LimitBottom = (int)GlobalPosition.y + (int)totalSize.y;

        Visible = false;
    }

}
