using Godot;
using System;

/* 
Script da usare per controllare lo stato generale del gioco:
- Gestire lo spawn dei nemici (qui chiami la funzione iniziale)
- Gestire il calcolo dei punteggi 
	(poi serve uno script dedicato per aggiornare la ui)
- ...
*/
public partial class MainController : Control
{
	[Export] private Button _continueButton;
	[Export] private Button _exitButton;
	private Label _scoreLabel;
	private GameManager _gameManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_continueButton.Pressed += () => HandleButtonClick("Continue");
		_exitButton.Pressed += () => HandleButtonClick("Exit");
		_scoreLabel = GetNode<Label>("../HUD/ScoreLabel");
		_gameManager = GetNode<GameManager>("/root/GameManager");
	}
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_scoreLabel.Text = 	$"Punteggio: {_gameManager.Score}";
	}

	private void HandleButtonClick(string action)
	{
		if (action == "Continue")
		{
			GetTree().Paused = false;
			Visible = GetTree().Paused;
		}
		if (action == "Exit")
		{
			_gameManager.Score = 0;
			GetTree().Paused = false;
			GetTree().ChangeSceneToFile("res://Scenes/menu.tscn");
		}
	}

	public override void _Input(InputEvent @event)
	{
		// per vedere la lista degli eventi esistenti/aggiungerne nuovi:
		// progetto -> impostazioni del progetto -> mappa di input
		if (@event.IsActionPressed("ui_cancel"))
		{
			GetTree().Paused = !GetTree().Paused;
			Visible = GetTree().Paused;
		}
	}
}
