using Godot;
using System;

public class Time : Label
{
    float milliSeconds = 0;
    int iMilliSeconds;
    int seconds = 0;
    int minutes = 0;

    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        milliSeconds +=  delta * 100;
        iMilliSeconds = Mathf.RoundToInt(milliSeconds);
        if (iMilliSeconds % 100 == 0 && iMilliSeconds > 0)
        {
            milliSeconds = 0;
            seconds++;

            if (seconds % 60 == 0)
            {
                GD.Print(seconds % 60);
                seconds = 0;
                minutes++;
            }
        }
        Text = minutes + " : " + seconds.ToString("D2") + " : " + iMilliSeconds.ToString("D2");
    }
}
