using Godot;

public class PauseMenu : CanvasLayer
{
	Godot.Button buttonContinue;
	Godot.Button buttonExitGame;

	private bool paused = false;

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int)KeyList.Escape)
		{
			if (Game.mainMenu.Visible)
				return;

			if (!paused)
			{
				PauseGame();
			}
			else
			{
				ResumeGame();
			}
		}
	}

	public override void _Ready()
	{
		buttonContinue = (Godot.Button)GetNode("%ButtonContinue");
		buttonContinue.Connect("pressed", this, nameof(OnButtonContinuePressed));

		buttonExitGame = (Godot.Button)GetNode("%ButtonExitGame");
		buttonExitGame.Connect("pressed", this, nameof(OnButtonExitGamePressed));

		Visible = false;
	}

	public void PauseGame()
	{
		paused = true;
		Input.SetMouseMode(Input.MouseModeEnum.Visible);
		GetTree().Paused = true;

		Visible = true;
	}

	public void ResumeGame()
	{
		Visible = false;
		paused = false;
		Input.SetMouseMode(Input.MouseModeEnum.Captured);
		GetTree().Paused = false;

		GD.Print("Prugh");

		/*
		var pauseMenu = GetTree().Root.GetNode("PauseMenu");
		pauseMenu.QueueFree();
		*/
	}

	private void OnButtonContinuePressed()
	{
		ResumeGame();
	}

	private void OnButtonExitGamePressed()
	{
		Game.mainMenu.Activate();
	}
}
