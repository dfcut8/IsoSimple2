using Godot;

namespace Scenes.Player;

public partial class Player : CharacterBody2D
{
    [Export]
    private float speed = 70.0f;

    private const float acceleration = 600.0f;
    private const float friction = 400.0f;

    private Vector2 direction = Vector2.Zero;

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
                Velocity = Vector2.Zero;
            }
        }

        Velocity += IsometricMovement(direction * acceleration * delta);
        MoveAndSlide();
    }
}
