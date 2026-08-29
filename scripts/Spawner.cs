using Godot;
using System;

public partial class Spawner : Node3D
{
	// Le scene possibili che possono spawnare: per ora solo i nemici e i buoni
	[Export] public PackedScene[] SpawnScenes;

	private Marker3D[] _spawnPoints;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		Timer timer = GetNode<Timer>("Timer"); // riferimento al nodo per recuperare il segnale di timeout
		timer.Timeout += OnTimerTimeout; // chiama la funzione allo scocco del timeout, += serve per non cancellare la chiamata precedente
		Node spawnPointsContainer = GetNode<Node>("../SpawnPoints");
		_spawnPoints = new Marker3D[spawnPointsContainer.GetChildCount()]; // array vuoto di marker3d di lunghezza figli di spawnpoints
		for (int i = 0; i < spawnPointsContainer.GetChildCount(); i++)
		{
			_spawnPoints[i] = spawnPointsContainer.GetChild<Marker3D>(i);
		}
	}
	
	
	private void OnTimerTimeout()
	{
		int randomIndex = GD.RandRange(0, _spawnPoints.Length - 1); // indice casuale per scegliere un punto spawn (RandRange prende min e max)
		/* 
			sceglie casualmente una scena dall'array delle scene. 
			per ora ci sono due scene e ognuna ha il 50% di possibilità di essere scelta
			non conosco altri metodi per ora da usare per scegliere con diverse probabilità ciascuna scena
		*/
		int randomSpawn = GD.RandRange(0, SpawnScenes.Length - 1);
		Target newTarget = SpawnScenes[randomSpawn].Instantiate<Target>(); // crea una nuova copia della scena casuale
		AddChild(newTarget); // aggiunta del nodo alla scena (prima era solo in memoria)
		newTarget.GlobalPosition = _spawnPoints[randomIndex].GlobalPosition; // posiziona target in punto spawn casuale
		
		// newTarget.IsGood = GD.Randf() < 0.3f; // il target sarà buono al 30%
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
