using Godot;

namespace Scenes.Interactables;

public partial class Crate : StaticBody2D
{
    private Area2D hitArea;
    private AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        hitArea = GetNode<Area2D>("%HitArea");
        animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");

        hitArea.AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D other)
    {
        // if (other.IsInGroup("Weapon"))
        // {
        //     animationPlayer.Play("Destroyed");
        //     animationPlayer.AnimationFinished += OnDestroyAnimationFinished;
        // }
        GD.Print($"AreaEntered. Other: {other.Name}");
        animationPlayer.Play("Destroyed");
        animationPlayer.AnimationFinished += OnDestroyAnimationFinished;
    }

    private void OnDestroyAnimationFinished(StringName animName)
    {
        if (!string.IsNullOrEmpty(animName) && animName == "Destroyed")
        {
            QueueFree();
        }
    }
}
