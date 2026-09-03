using Godot;
using System;

public partial class GameOverButtons : HBoxContainer
{
	
	[Export] private Button _backToMenuButton;
	private GameManager _gameManager;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_backToMenuButton.Pressed += () => HandleButtonClick("Menu");
		_gameManager = GetNode<GameManager>("/root/GameManager");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private void HandleButtonClick(string action)
	{
		if (action == "Menu")
		{
			_gameManager.Score = 0;
			GetTree().ChangeSceneToFile("res://Scenes/menu.tscn");
		}
	}
}
