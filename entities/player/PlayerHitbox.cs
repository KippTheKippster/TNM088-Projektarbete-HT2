using Godot;
using System;

public class PlayerHitbox : Area2D
{

    public override void _Ready()
    {
    }

    private void _on_Hitbox_area_entered(Area2D area)
    {
        GD.Print(area.GetGroups());
        if (area.IsInGroup("hurt") && !Game.player.godMode)
        {
            if (area.IsInGroup("stompable"))
            {
                GD.Print(Game.player.externalSpeed.y);
                if (!Game.player.IsOnFloor())
                {
                    return;
                }
            }

            PlayerDeath deathType = PlayerDeath.Default;

            if (area.IsInGroup("electricity"))
            {
                deathType = PlayerDeath.Electricity;
            }

            Game.player.Kill(deathType);
        }
    }
}
