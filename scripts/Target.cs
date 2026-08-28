using Godot;
using System;

public partial class Target : PhysicalBone3D
{
	[Export] public bool IsGood = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
