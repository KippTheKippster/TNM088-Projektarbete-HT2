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

public class LevelStipulation : Node
{
	internal Level level;

	public override void _Ready()
    {
		level = GetParent<Level>();
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
	ButtonListener buttonListener;


	public override void _Ready()
    {
        base._Ready();
		buttonListener = (ButtonListener)GD.Load<PackedScene>("res://listeners/ButtonListener.tscn").Instance();
		AddChild(buttonListener);
		buttonListener.id = "EndElevator";
		buttonListener.Connect("SignalAllPressed", this, "OnAllPressed");
	}

	private void OnAllPressed()
    {
		GD.Print("AllPressed!");
		level.Open();
	}
}


public class Level : Node2D
{
	[Signal] public delegate void SignalOpen();
	[Signal] public delegate void SignalEnemyAdd(string id);
	[Signal] public delegate void SignalEnemyDied(string id);
	[Signal] public delegate void SignalNextLevel();
	[Signal] public delegate void SignalRestart();
	[Signal] public delegate void SignalButtonPressed(string id);
	[Signal] public delegate void SignalButtonAdd(string id);

	public Elevator startElevator;
	public Elevator endElevator;
	public LevelStipulation levelStipulation;

	[Export(PropertyHint.Enum, "None,Kill all enemies,Hit all buttons,Custom")] int levelStipulationNumb;

	public void Ready()
	{
		startElevator = GetNode<Elevator>("%StartElevator");
		endElevator = GetNode<Elevator>("%EndElevator");

		SetStipulation();

		Connect("SignalEnemyDied", levelStipulation, "OnEnemyDied");
		Connect("SignalEnemyAdd", levelStipulation, "OnEnemyAdd");
		endElevator.Connect("SignalNextLevel", this, nameof(NextLevel));

		GD.Print("Level Ready");

		Game.player.Visible = true;
		Game.player.externalSpeed = Vector2.Zero;
		Game.player.currentAmmo = Game.player.maxAmmo;
		Game.player.StopCharge();
	}

	private void OnEnemyAdd()
    {
    }

	private void NextLevel()
	{
		EmitSignal(nameof(SignalNextLevel));
	}

	private void SetStipulation()
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
			case 3:
				{
					levelStipulation = new LevelStipulation();
					break;
				}
		}

		AddChild(levelStipulation);
	}

	public void Open()
	{
		endElevator.Open();
	}
}
