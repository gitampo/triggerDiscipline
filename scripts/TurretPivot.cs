using Godot;
using System;

public partial class TurretPivot : Node3D
{
	private Camera3D _camera;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_camera = GetNode<Camera3D>("Camera3D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mousePos = GetViewport().GetMousePosition();
		Vector3 rayOrigin = _camera.ProjectRayOrigin(mousePos);
		Vector3 rayDirection = _camera.ProjectRayNormal(mousePos);
		Vector3 rayEnd = rayOrigin + (rayDirection * 1000f);
		PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
		PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(rayOrigin, rayEnd); 
		Godot.Collections.Dictionary result = spaceState.IntersectRay(query);

		if (result.Count > 0)
		{
			Vector3 targetPoint = (Vector3)result["position"];
		}
	}
}
