using Godot;
using System;

public partial class Crosshair : TextureRect
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Vector2 screenCenter = GetViewport().GetVisibleRect().Size / 2;
		Position = screenCenter - Size / 2;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
