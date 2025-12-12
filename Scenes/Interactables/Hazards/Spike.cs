using Godot;

namespace Scenes.Interactables.Hazards;

public partial class Spike : Area2D
{
    public override void _Ready()
    {
        BodyEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Node2D other)
    {
        GD.Print($"Spike: AreaEntered triggered by ${other.Name}");
    }
}
