using Godot;
using System;
using System.ComponentModel;
using System.Threading.Tasks.Dataflow;

public partial class TurretPivot : Node3D
{
	private Camera3D _camera;
	private Camera3D _aimReference;
	private MainController _mainController;
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_camera = GetNode<Camera3D>("Camera3D");
		_aimReference = GetNode<Camera3D>("../AimReference");
		_mainController = GetNode<MainController>("../Pausa");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mousePos = GetViewport().GetMousePosition();
		Vector3 rayOrigin = _aimReference.ProjectRayOrigin(mousePos);
		Vector3 rayDirection = _aimReference.ProjectRayNormal(mousePos);

 		Vector3 planeNormal = -_aimReference.GlobalTransform.Basis.Z;
		Plane aimPlane = new(planeNormal, _aimReference.GlobalPosition + planeNormal * 10f);	

		Vector3? intersection = aimPlane.IntersectsRay(rayOrigin, rayDirection);

		if (intersection.HasValue)
   		{
			LookAt(intersection.Value, Vector3.Up);
		}

		if (Input.IsActionJustPressed("shoot"))
		{
			GD.Print("Sparato!");

			Vector3 shootOrigin = _camera.ProjectRayOrigin(mousePos);
			Vector3 shootDirection = _camera.ProjectRayNormal(mousePos);
			Vector3 rayEnd = shootOrigin + (shootDirection * 1000f);

			PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
			PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(shootOrigin, rayEnd); 
			Godot.Collections.Dictionary result = spaceState.IntersectRay(query);

			
			if (result.Count > 0)
			{
				GodotObject colliderObj = result["collider"].As<GodotObject>();
				Target hitTarget = colliderObj as Target;
				GD.Print("Nodo colpito: ", result["collider"]);
				
				if (hitTarget != null){
					if (hitTarget.IsGood)
					{
						GD.Print("Hai colpito un buono! Game Over!");
						GetTree().ChangeSceneToFile("res://scenes/gameOver.tscn");
					}
					else 
					{
						GD.Print("Nemico colpito!");
						_mainController.score += 10;
						_mainController.UpdateScoreLabel();
						
					}
					
					hitTarget.QueueFree();
				}
				
			}
		}
	}
}
