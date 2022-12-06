using Godot;

public class PauseMenu : Node
{
	private bool paused = false;

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int)KeyList.Escape)
		{
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

	private void PauseGame()
	{
		paused = true;
		Input.SetMouseMode(Input.MouseModeEnum.Visible);
		GetTree().Paused = true;

		var menu = (PackedScene)GD.Load("res://ui/pause_menu.tscn");
		var pauseMenu = (Node)menu.Instance();
		GetTree().Root.AddChild(pauseMenu);

		var buttonHowToPlay = (Button)pauseMenu.GetNode("ButtonHowToPlay");
		buttonHowToPlay.Connect("pressed", this, nameof(OnButtonHowToPlayPressed));


		var buttonAbout = (Button)pauseMenu.GetNode("ButtonAbout");
		buttonAbout.Connect("pressed", this, nameof(OnButtonAboutPressed));

		var buttonExitGame = (Button)pauseMenu.GetNode("ButtonExitGame");
		buttonExitGame.Connect("pressed", this, nameof(OnButtonExitGamePressed));
	}

	private void ResumeGame()
	{
		paused = false;
		Input.SetMouseMode(Input.MouseModeEnum.Captured);
		GetTree().Paused = false;

		var pauseMenu = GetTree().Root.GetNode("PauseMenu");
		pauseMenu.QueueFree();
	}

	private void OnButtonHowToPlayPressed()
	{
		// TODO: Display the "How to Play" screen.
	}

	private void OnButtonAboutPressed()
	{
		// TODO: Display the "About" screen.
	}

	private void OnButtonExitGamePressed()
	{
		GetTree().Quit();
	}
}
