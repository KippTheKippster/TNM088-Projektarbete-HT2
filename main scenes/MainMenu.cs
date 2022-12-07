using Godot;
using System;

public class MainMenu : CanvasLayer
{
	Game game;
	Ui ui;

	public override void _Ready()
	{
		game = GetParent<Game>();
		Input.SetMouseMode(Input.MouseModeEnum.Visible);
	}

	
	private void _on_Timer_timeout()
	{
		Activate();
	}


	private void _on_Start_button_up()
	{
		DeActivate();
	}

	private void _on_Exit_button_up()
	{
		GetTree().Quit();
	}

	public void Activate()
	{
		ui = Game.ui;
		ui.Visible = false;
		Game.player.Visible = false;
		Game.player.active = false;
		Visible = true;
		Game.pauseMenu.ResumeGame();
		Input.SetMouseMode(Input.MouseModeEnum.Visible);
		Game.time.active = false;
		Game.time.seconds = 0;
		Game.time.minutes = 0;
		Game.time.iMilliSeconds = 0;
		Game.time.milliSeconds = 0;

        Game.levelNumber = 300;

		if (Game.level != null)
		{
			Game.level.QueueFree();
			Game.level = null;
		}
	}

	public void DeActivate()
	{
		game.NextLevel();
		ui.Visible = true;
		Visible = false;
		Game.player.Visible = true;
		Game.player.active = true;
		Game.time.active = true;
		Input.SetMouseMode(Input.MouseModeEnum.Captured);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
