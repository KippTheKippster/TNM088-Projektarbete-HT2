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

    public override void _Ready()
    {
        Playing = true;

        if (!EndElevator)
        {
            Game.player.GlobalPosition = GlobalPosition + Vector2.Down * 16;
            Game.player.active = false;
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
        Open();
    }

    public override void _Process(float delta)
    {
        if (!Engine.EditorHint)
        {
            //RotationDegrees += 180 * delta;
        }
    }

    public void Open()
    {
        Animation = "opening";
    }
    public void Close()
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
            Close();
            EmitSignal("SignalNextLevel");
            GD.Print("NextLevel");
        }
    }

    public void OpenEndElevator()
    {
        Open();
        GetNode<Timer>("Timer").Start();
        _on_Timer_timeout();
    }

    private void _on_Timer_timeout()
    {
        Circle circle = (Circle)GD.Load<PackedScene>("res://effects/Circle.tscn").Instance();
        AddChild(circle);
        GD.Print("DJWAPOI10");
    }
}
