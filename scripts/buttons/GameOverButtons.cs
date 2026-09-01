using Godot;
using System;

public partial class GameOverButtons : HBoxContainer
{
	
	[Export] private Button _backToMenuButton;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_backToMenuButton.Pressed += () => HandleButtonClick("Menu");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private void HandleButtonClick(string action)
	{
		if (action == "Menu")
		{
			GetTree().ChangeSceneToFile("res://Scenes/menu.tscn");
		}
	}
}
