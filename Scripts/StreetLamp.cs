using Godot;
using System;

public partial class StreetLamp : Node3D
{
	[Export] public SpotLight3D LightSource;
	// Da implementare: diversi numeri per diversi lampioni
	// Es: un lampione con 0 è quasi sempre spento, un lampione con 1 è sempre acceso
	// e tutti gli stati intermedi
	[Export(PropertyHint.Range, "0,1,0.01")] public float WorkingChance = 0.9f;

	// Quanto è luminoso (più di 1 non ci sta bene)
	private float energy = 1f;
	// Tempo rimanente al prossimo cambio di stato on/off
	private double remainingTime;
	private bool isLightOn;

	private enum LightStates
	{
		Stable, // Luce normale, lampeggia occasionalmente
		Unstable // Lampeggia di più
	};
	private LightStates currentState = LightStates.Stable;
	// Tempo rimanente al prossimo cambiamento di stato stabile/non
	private double stateTimer;

	// Intervallo in secondi per l'accensione delle luci stabili
	private float minimumStableInterval = 1.0f;
	private float maximumStableInterval = 10.0f;

	// Intervallo in secondi per l'accensione delle luci instabili
	private float minimumUnstableInterval = 0.1f;
	private float maximumUnstableInterval = 0.3f;

	// Intervallo in secondi per lo spegnimento delle luci stabili
	private float minimumStableOffInterval = 0.5f;
	private float maximumStableOffInterval = 1.5f;

	// Durata unica della durata spegnimento delle luci instabili
	private float unstableOffInterval = 0.1f;

	// Energia minima/massima del lampione
	private float minimumEnergy = 0.3f;
	private float maximumEnergy = 1.0f;

	[Export] private int UnstableDuration = 3;
	[Export] private int StableDuration = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (LightSource == null)
		{
			GD.PushError($"Light source not added: {Name}");
			SetProcess(false);
			return; 
		}

		// Luminosità casuale per lampione (non cambia, ma si può sovrascrivere per singolo lampione)
		energy = (float)GD.RandRange(minimumEnergy, maximumEnergy);

		TurnOn();
		CreateRandomState();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (stateTimer > 0)
		{
			stateTimer -= delta;
		}
		else
		{
			CreateRandomState();
		}

		if (remainingTime > 0)
		{
			remainingTime -= delta;
		}
		else
		{
			if (currentState == LightStates.Stable)
			{
				HandleTurnOnOff();

				// Solo quando la luce è stabile, lo spegnimento deve durare massimo 2 secondi
				if (!isLightOn)
				{
					remainingTime = GD.RandRange(minimumStableOffInterval, maximumStableOffInterval);
				}
				else
				{   // Durata luce accesa stabile
					remainingTime = GD.RandRange(minimumStableInterval, maximumStableInterval);
				}
			}
			else if (currentState == LightStates.Unstable)
			{
				HandleTurnOnOff();

				// La luce spenta instabile dura sempre unstableOffInterval
				if (!isLightOn)
				{
					remainingTime = unstableOffInterval;
				}
				else
				{   // Durata luce accesa instabile
					remainingTime = GD.RandRange(minimumUnstableInterval, maximumUnstableInterval);
				}
			}
		}
	}

	private void HandleTurnOnOff()
	{
		// cambia intensità
		if (!isLightOn)
		{
			TurnOn();
		}
		else
		{
			TurnOff();
		}
	}

	private void TurnOn()
	{
		LightSource.LightEnergy = energy;
		isLightOn = true;
	}

	private void TurnOff()
	{
		LightSource.LightEnergy = 0;
		isLightOn = false;
	}

	/*
		Crea uno stato casuale stabile/instabile 
		basato sul valore di workingChance. 
		Un valore WorkingChange di 0 ha il 30% di ottenere una luce funzionante;
		Un valore di 1 ha un 95% di una luce funzionante
	*/
	private void CreateRandomState()
	{
		float stableChance = Mathf.Lerp(30f, 95f, WorkingChance);
		double random = GD.RandRange(0, 100);

		if (random <= stableChance)
		{
			currentState = LightStates.Stable;
			stateTimer = StableDuration;
		}
		else
		{
			currentState = LightStates.Unstable;
			stateTimer = UnstableDuration;
		}
	}
}
