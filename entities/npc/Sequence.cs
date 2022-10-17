using Godot;
using System;

public class Sequence : Node
{
    [Export] readonly string text;
    [Export] readonly string[] signals;

    public void Start()
    {
        if (signals != null)
            for (int i = 0; i < signals.Length; i++)
                EmitSignal(signals[i]); 

        if (text != null)
            Ui.dialogue.UpdateText(text);
    }
}
