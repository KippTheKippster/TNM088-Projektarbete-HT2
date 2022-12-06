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
                if (Game.player.externalSpeed.y > 0 || Game.player.externalSpeed.y < -0.1f)
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