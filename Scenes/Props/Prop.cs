using Godot;
using Scenes.Players;

namespace Scenes.Props;

public partial class Prop : StaticBody2D
{
    private Area2D detector;

    public override void _Ready()
    {
        detector = GetNode<Area2D>("%Detector");
        detector.BodyEntered += OnBodyEntered;
        detector.BodyExited += OnBodyExited;
    }

    private void OnBodyExited(Node2D body)
    {
        if (body is not Player)
            return;
        var color = Modulate;
        color.A = 1f;
        Modulate = color;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is not Player)
            return;
        GD.Print("Entered prop area");
        var color = Modulate;
        color.A = 0.5f;
        Modulate = color;
    }
}
