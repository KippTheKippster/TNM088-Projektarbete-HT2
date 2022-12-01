using Godot;
using System;

public abstract class Listener : Node
{
    [Signal] public delegate void SignalReached();
    [Export] public string id;
    public int count;
    public int maxCount;

    public override void _Ready()
    {
        Connect(nameof(SignalReached), GetParent(), "Open");
    }

    internal virtual void OnActivate(string id)
    {
        if (this.id == id)
        {
            count++;

            if (count >= maxCount)
            {
                EmitSignal(nameof(SignalReached));
            }
        }
    }

    internal virtual void OnAdd(string id)
    {
        if (this.id == id)
            maxCount++;
    }
}
