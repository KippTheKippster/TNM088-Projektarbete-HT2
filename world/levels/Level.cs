using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/*
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ExportEnumAsFlagsAttribute : ExportAttribute
{
	public ExportEnumAsFlagsAttribute(Type enumType) : base(PropertyHint.Enum, enumType.IsEnum ? string.Join(",", Enum.GetValues(enumType).OfType<Enum>().Where(e => (int)(object)e != 0)) : "Invalid Type")
	{
	}
}
*/
//[ExportEnumAsFlags(typeof(LevelWinStipulate))] readonly int levelWinStipulate;	//[ExportEnumAsFlags(typeof(LevelWinStipulate))] readonly int levelWinStipulate;

public abstract class LevelStipulation : Node
{
	internal Level level;

	public override void _Ready()
    {
		level = (Level)GetParent();
    }

	public virtual void OnEnemyDied() {}
	public virtual void OnEnemyAdd() {}
}

public class NoneStipulation : LevelStipulation
{
    public override void _Ready()
    {
		base._Ready();
		level.Open();
	}
}

public class KillAllStipulation : LevelStipulation
{
	int maxEnemyCount;
	int enemyCount;

	public override void OnEnemyDied()
	{
		enemyCount--;
		if (enemyCount <= 0)
		{
			//EmitSignal("SignalOpen");
			level.Open();
		}
	}

    public override void OnEnemyAdd()
    {
        enemyCount++;
        maxEnemyCount++;

		GD.Print("ONENEMYADD");
    }
}

public class HitButtonsStipulation : LevelStipulation
{
	public override void OnEnemyAdd()
	{
	}
}


public class Level : Node2D
{
	[Signal] public delegate void SignalOpen();
	[Signal] public delegate void SignalEnemyAdd();
	[Signal] public delegate void SignalEnemyDied();
	[Signal] public delegate void SignalNextLevel();

	public Elevator startElevator;
	public Elevator endElevator;
	public LevelStipulation levelStipulation;

	[Export(PropertyHint.Enum, "None,Kill all enemies,Hit all buttons")] int levelStipulationNumb;

	public override void _Ready()
	{
		startElevator = GetNode<Elevator>("%StartElevator");
		endElevator = GetNode<Elevator>("%EndElevator");

		GetStipulation();

		Connect("SignalEnemyDied", levelStipulation, "OnEnemyDied");
		Connect("SignalEnemyAdd", levelStipulation, "OnEnemyAdd");
		endElevator.Connect("SignalNextLevel", this, nameof(NextLevel));
	}

	private void NextLevel()
    {
		EmitSignal(nameof(SignalNextLevel));
    }

	private void GetStipulation()
    {
		switch (levelStipulationNumb)
		{
			case 0:
				{
					levelStipulation = new NoneStipulation();
					break;
				}
			case 1:
				{
					levelStipulation = new KillAllStipulation();
					break;
				}
			case 2:
				{
					levelStipulation = new HitButtonsStipulation();
					break;
				}
		}

		AddChild(levelStipulation);
	}

	public void Open()
	{
		endElevator.Open();
	}

	void OnEnemyAdd()
    {
		GD.Print("SELFENEMYADD");
    }
}
