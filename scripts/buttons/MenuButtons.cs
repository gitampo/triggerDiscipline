using Godot;
using System;

public partial class MenuButtons : Node
{
	[Export] private Button _startButton;
	[Export] private Button _optionsButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// += iscrive una funzione a un evento (segnale)
		_startButton.Pressed += () => HandleButtonClick("Start");
		_optionsButton.Pressed += () => HandleButtonClick("Options");
	}

	private void HandleButtonClick(string action)
	{
		if (action == "Start")
		{
			GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
		}
		if (action == "Options")
		{
			GD.Print("Opzioni");
		}
	}
}
