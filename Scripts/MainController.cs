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
	public float score = 0;
	public bool isGameRunning = false;
	[Export] private Button _continueButton;
	[Export] private Button _exitButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_continueButton.Pressed += () => HandleButtonClick("Continue");
		_exitButton.Pressed += () => HandleButtonClick("Exit");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

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
			score = 0;
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
