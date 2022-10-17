using Godot;
using System;

public class Dialogue : Label
{
    private Timer _letterUpdate;

    private int _letterAmount;

    public override void _Ready()
    {
        _letterUpdate = GetNode<Timer>("%LetterUpdate");
        UpdateText("Testing 123 i love beans");
    }

    public void UpdateText(string text)
    {
        Text = text;
        _letterAmount = text.Length;
        VisibleCharacters = 0;
        _letterUpdate.Start();
    }

    private void OnLetterUpdate()
    {
        VisibleCharacters++;
        
        if (VisibleCharacters >= _letterAmount)
            _letterUpdate.Stop();
    }
}   
