using Godot;
using System;

public class SequenceHandler : Node
{
    private int _sequenceNumber;
    public int SequenceNumber 
    {
        get
        {
            return _sequenceNumber;
        }
        set 
        {
            _sequenceNumber = value;
            SetSequence(value);
        }
    }

    private void SetSequence(int numb)
    {
        if (numb < 0 || numb > GetChildren().Count)
        {
            GD.PrintErr("Tried to access a sequence that doesn't exist! Sequence number: " + numb);
            return;
        }

        Sequence sequence = GetChild<Sequence>(numb);
        sequence.Start();
    }

    private void _on_Timer_timeout()
    {
        SequenceNumber = 0;
    }
}
