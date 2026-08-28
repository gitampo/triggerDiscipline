using Godot;
using System;

/* 
Script da usare per controllare lo stato generale del gioco:
- Gestire lo spawn dei nemici (qui chiami la funzione iniziale)
- Gestire il calcolo dei punteggi 
	(poi serve uno script dedicato per aggiornare la ui)
- ...
*/
public partial class MainController : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
