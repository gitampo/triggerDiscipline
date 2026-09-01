using Godot;
using System;

public partial class Spawner : Node3D
{
	// Le scene possibili che possono spawnare: per ora solo i nemici e i buoni
	[Export] public PackedScene[] SpawnScenes;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Timer timer = GetNode<Timer>("Timer"); // riferimento al nodo per recuperare il segnale di timeout
		timer.Timeout += OnTimerTimeout; // chiama la funzione allo scocco del timeout, += serve per non cancellare la chiamata precedente
	}
	
	private void OnTimerTimeout()
	{
		// Per ogni scena che viene creata creo delle coordinate casuali (solo per la x, ovvero la posizione in orizzontale)
		int width = 9;
		int spawnWidth = GD.RandRange(-width, width);

		/* 
			sceglie casualmente una scena dall'array delle scene. 
			per ora ci sono due scene e ognuna ha il 50% di possibilità di essere scelta
			non conosco altri metodi per ora da usare per scegliere con diverse probabilità ciascuna scena
		*/
		int randomSceneSpawn = GD.RandRange(0, SpawnScenes.Length - 1);
		Target newTarget = SpawnScenes[randomSceneSpawn].Instantiate<Target>(); // crea una nuova copia della scena casuale
		AddChild(newTarget); // aggiunta del nodo alla scena (prima era solo in memoria)
		
		// Creo un nuovo vector3 con: la spawnWidth creata casualmente, un'altezza di 0.5 da terra e -8 di distanza
		// 0.5 e -8 erano i valori che avevano gli spawnPoint prima
		newTarget.GlobalPosition = new Vector3(spawnWidth, 0.5f, -8); // posiziona target in punto spawn casuale
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
