using Godot;
using System;

[Tool]
public class Elevator : AnimatedSprite
{
    private bool _endElevator;
    [Export] public bool EndElevator
    {
        get
        {
            return _endElevator;
        }
        set
        {
            _endElevator = value;

            if (value)
                Animation = "closed";
            else
                Animation = "open";
        }
    }

    [Signal] public delegate void SignalNextLevel();

    CollisionShape2D collisionShape;

    public override void _Ready()
    {
        Playing = true;
        collisionShape = GetNode<CollisionShape2D>("%CollisionShape2D");
        collisionShape.Disabled = true;

        if (!EndElevator)
        {
            Game.player.GlobalPosition = GlobalPosition + Vector2.Down * 16;
            Game.player.active = false;
            Game.player.velocity = Vector2.Zero;
            Game.player.externalSpeed = Vector2.Zero;
            Game.player.moveSpeed = Vector2.Zero;
            Game.player.targetMoveSpeed = Vector2.Zero;
            Game.player.gun.Visible = true;
            Game.player.Visible = true;
            Game.player.model.Animation = "Idle";
            ZIndex = 6;

            Timer timer = new Timer();
            AddChild(timer);
            timer.WaitTime = 0.1f;
            timer.OneShot = true;
            timer.Connect("timeout", this, nameof(Start));
            timer.Start();

            Animation = "closed";
        }
    }

    private void Start()
    {
        GD.Print("Starting");
        OpenElevator();
    }

    public override void _Process(float delta)
    {
        if (!Engine.EditorHint)
        {
            //RotationDegrees += 180 * delta;
        }
    }

    public void OpenElevator()
    {
        Animation = "opening";
    }
    public void CloseElevator()
    {
        Animation = "closing";
    }

    private void OnAnimationFinished()
    {
        if (Animation == "opening")
        {
            Animation = "open";

            if (!_endElevator)
            {
                Game.player.active = true;
                ZIndex = -1;
            }
        }

        if (Animation == "closing")
        {
            Animation = "closed";
        }
    }

    private void OnBodyEntered(Node body)
    {
        if (body.IsInGroup("Player") && EndElevator && Animation == "open")
        {
            Player player = (Player)body;
            player.GlobalPosition = GlobalPosition + 16 * Vector2.Down;
            player.active = false;
            ZIndex = 6;
            CloseElevator();
            player.model.Animation = "Idle";

            Timer timer = new Timer();
            AddChild(timer);
            timer.WaitTime = 0.4f;
            timer.OneShot = true;
            timer.Connect("timeout", this, nameof(NextLevel));
            timer.Start();
        }
    }

    public void Open()
    {
        OpenElevator();
        GetNode<Timer>("Timer").Start();
        _on_Timer_timeout();
        //collisionShape.Disabled = false;
        collisionShape.SetDeferred("disabled", false);
    }

    private void _on_Timer_timeout()
    {
        Circle circle = (Circle)GD.Load<PackedScene>("res://effects/Circle.tscn").Instance();
        AddChild(circle);
    }

    private void NextLevel()
    {
        EmitSignal("SignalNextLevel");
        GD.Print("NextLevel");
    }
}
