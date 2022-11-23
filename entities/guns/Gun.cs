using Godot;
using System;

public class Gun : Node2D
{
    [Export] private readonly PackedScene bulletScene;
    [Export] public float Speed { get; set; } = 200f;
    [Export] public int Damage { get; set; } = 1;
    [Export] public bool RotateSprite { get; set; } = true;

    private Bullet CreateBullet(Vector2 position)
    {
        Bullet bullet = (Bullet)bulletScene.Instance();
        Game.bulletSpace.AddChild(bullet);
        bullet.GlobalPosition = position;
        bullet.RotateSprite = RotateSprite;
        bullet.Speed = Speed;
        bullet.Damage = Damage;

        return bullet;
    }

    public virtual Bullet Shoot(Vector2 position, float angle = 0)
    {
        Bullet bullet = CreateBullet(position);
        bullet.Angle = angle;

        return bullet;
    }

    public virtual Bullet Shoot(Vector2 position, Vector2 moveDir)
    {
        Bullet bullet = CreateBullet(position);
        bullet.MoveVector = moveDir;

        return bullet;
    }
}
