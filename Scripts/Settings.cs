using Godot;
using System;

public partial class Settings : Control
{
	private GameManager _gameManager;
	private Label _totalPointsLabel;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_gameManager = GetNode<GameManager>("/root/GameManager");
		_totalPointsLabel = GetNode<Label>("VBoxContainer/CanvasLayer/TotalPointsLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_totalPointsLabel.Text = 	$"Punteggio totale: {_gameManager.TotalPoints}";
	}
}
