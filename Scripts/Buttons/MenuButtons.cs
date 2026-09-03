using Godot;
using System;

public partial class MenuButtons : Node
{
	[Export] private Button _startButton;
	[Export] private Button _optionsButton;
	[Export] private Button _closeGameButton;
	private GameManager _gameManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// += iscrive una funzione a un evento (segnale)
		_startButton.Pressed += () => HandleButtonClick("Start");
		_optionsButton.Pressed += () => HandleButtonClick("Options");
		_closeGameButton.Pressed += () => HandleButtonClick("Close");
		_gameManager = GetNode<GameManager>("/root/GameManager");
	}

	private void HandleButtonClick(string action)
	{
		if (action == "Start")
		{
			GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
		}
		if (action == "Options")
		{
			GetTree().ChangeSceneToFile("res://Scenes/settings.tscn");
		}
		if (action == "Close")
		{
			GetTree().Quit();
		}
	}
}
