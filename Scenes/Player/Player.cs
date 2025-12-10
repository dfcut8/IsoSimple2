using Godot;

namespace Scenes.Player;

public partial class Player : CharacterBody2D
{
    [Export]
    private float speed = 70.0f;

    private const float acceleration = 600.0f;
    private const float friction = 400.0f;

    // Nodes and states
    private AnimationTree animationTree;
    private AnimationNodeStateMachinePlayback animationStateMachine;

    private Vector2 direction = Vector2.Zero;

    public override void _Ready()
    {
        animationTree = GetNode<AnimationTree>("AnimationTree");
        animationStateMachine = (AnimationNodeStateMachinePlayback)
            animationTree.Get("parameters/playback");
    }

    public override void _Process(double delta)
    {
        PlayerMovement((float)delta);
    }

    private Vector2 IsometricMovement(Vector2 direction)
    {
        var adjustedDirection = Vector2.Zero;
        adjustedDirection.X = direction.X - direction.Y;
        adjustedDirection.Y = (direction.X + direction.Y) / 2;
        return adjustedDirection;
    }

    private void PlayerMovement(float delta)
    {
        direction = Input.GetVector(
            "player_move_left",
            "player_move_right",
            "player_move_up",
            "player_move_down"
        );

        if (direction != Vector2.Zero)
        {
            GD.Print("Moving");
            UpdateAnimationState(direction);
            Velocity = Velocity.LimitLength(speed);
        }

        if (direction == Vector2.Zero)
        {
            if (Velocity.Length() > (friction * delta))
            {
                Velocity -= Velocity.Normalized() * (friction * delta);
            }
            else
            {
                GD.Print("Idling");
                animationStateMachine.Travel("Idle");
                Velocity = Vector2.Zero;
            }
        }

        Velocity += IsometricMovement(direction * acceleration * delta);
        MoveAndSlide();
    }

    private void UpdateAnimationState(Vector2 direction)
    {
        animationTree.Set("parameters/Idle/blend_position", direction);
        // animationTree.Set("parameters/Move/blend_position", direction);
        // animationTree.Set("parameters/Attack/blend_position", direction);
    }
}
