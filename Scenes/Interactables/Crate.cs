using Godot;

namespace Scenes.Interactables;

public partial class Crate : StaticBody2D
{
    private Area2D hitArea;
    private AnimationPlayer animationPlayer;
    private CrateState currentState = CrateState.Exists;

    private enum CrateState
    {
        Exists,
        Destroying,
    }

    public override void _Ready()
    {
        hitArea = GetNode<Area2D>("%HitArea");
        animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");

        hitArea.AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D other)
    {
        GD.Print($"AreaEntered. Other: {other.Name}");
        if (currentState == CrateState.Destroying)
        {
            return;
        }
        animationPlayer.Play("Destroyed");
        animationPlayer.AnimationFinished += OnDestroyAnimationFinished;
        currentState = CrateState.Destroying;
    }

    private void OnDestroyAnimationFinished(StringName animName)
    {
        if (!string.IsNullOrEmpty(animName) && animName == "Destroyed")
        {
            QueueFree();
        }
    }
}
