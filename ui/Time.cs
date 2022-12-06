using Godot;
using System;

public class Time : Label
{
    float milliSeconds = 0;
    int iMilliSeconds;
    int seconds = 0;
    int minutes = 0;

    public bool active = true;

    public override void _Process(float delta)
    {
        if (!active)
            return;

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
