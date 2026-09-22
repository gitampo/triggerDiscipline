using Godot;
using System;
using System.ComponentModel;
using System.Threading.Tasks.Dataflow;

public partial class TurretPivot : Node3D
{
	private Camera3D _camera;
	private Camera3D _aimReference;
	private GameManager _gameManager;
	private float _yaw = 0f;
	private float _pitch = 0f;
	private float sensitivity = 0.1f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_camera = GetNode<Camera3D>("Camera3D");
		_aimReference = GetNode<Camera3D>("../AimReference");
		_gameManager = GetNode<GameManager>("/root/GameManager");
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RotationDegrees = new Vector3(0, _yaw, 0);
		_camera.RotationDegrees = new Vector3(_pitch, 0, 0);

		if (Input.IsActionJustPressed("shoot"))
		{
			GD.Print("Sparato!");

			Vector3 shootOrigin = _camera.GlobalPosition;
			Vector3 shootDirection = -_camera.GlobalTransform.Basis.Z;
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
						_gameManager.Score += 10;
						_gameManager.TotalPoints += 10;
					}
					
					hitTarget.QueueFree();
				}	
			}
		}
	}
	
	public override void _Input(InputEvent @event){
		
		if(@event is InputEventMouseMotion mouseMotion){
			
 		_yaw -= mouseMotion.Relative.X * sensitivity;
		_yaw = Mathf.Clamp(_yaw, -80f, 80f);
		_pitch -= mouseMotion.Relative.Y * sensitivity;
		_pitch = Mathf.Clamp(_pitch, -80, 80);
		}
	}
}
