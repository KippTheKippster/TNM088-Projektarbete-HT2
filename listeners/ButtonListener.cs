using Godot;
using System;

public class ButtonListener : Node
{
    [Signal] public delegate void SignalAllPressed();

    [Export] public string id;

    public int buttonCount;
    public int buttonPressedCount;

    public override void _Ready()
    {
        Game.level.Connect("SignalButtonPressed", this, nameof(OnPressed));
        Game.level.Connect("SignalButtonAdd", this, nameof(OnAdd));
    }

    private void OnPressed(string id)
    {
        if (this.id == id)
        {
            GD.Print("PRESSED: " + id);
            buttonPressedCount++;

            if (buttonPressedCount >= buttonCount)
            {
                EmitSignal(nameof(SignalAllPressed));
            }
        }
    }

    private void OnAdd(string id)
    {
        if (this.id == id)
            buttonCount++;

        GD.Print("ONADD");
    }
}
