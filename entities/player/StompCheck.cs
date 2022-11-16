using Godot;
using System;

public class StompCheck : Area2D
{
    private Player player;

    [Export] public readonly float jumpStrength = 200;

    public override void _Ready()
    {
        player = GetParent<Player>();
    }

    private void OnStompCheckEntered(Area2D area)
    {
        if (area.GetParent().IsInGroup("stompable"))
        {
            if (player.velocity.y > 0)
            {
                ((Enemy)area.GetParent()).Kill();
                player.externalSpeed.y = -jumpStrength  ;
            }
        }
    }
}
