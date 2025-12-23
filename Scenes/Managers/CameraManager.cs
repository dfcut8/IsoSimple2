using Godot;

namespace Scenes.Managers;

public partial class CameraManager : Node2D
{
    [Export]
    private Node levelLevelCameraBoundaries;

    [Export]
    private Node2D followCameraTarget;
    override public void _Ready()
    {
        var followCamera = new Camera2D();
        var top = levelLevelCameraBoundaries.GetNode<Marker2D>("Top");
        var bottom = levelLevelCameraBoundaries.GetNode<Marker2D>("Bottom");
        var left = levelLevelCameraBoundaries.GetNode<Marker2D>("Left");
        var right = levelLevelCameraBoundaries.GetNode<Marker2D>("Right");
        followCamera.Zoom = new Vector2(4f, 4f);
        followCamera.LimitEnabled = true;
        followCamera.LimitTop = (int)top.Position.Y;
        followCamera.LimitBottom = (int)bottom.Position.Y;
        followCamera.LimitRight = (int)right.Position.X;
        followCamera.LimitLeft = (int)left.Position.X;
        followCameraTarget.AddChild(followCamera);
    }
}
