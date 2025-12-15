using Godot;

namespace Scenes.Players;

public static class PlayerState
{
    public static float CurrentLives { get; private set; } = 3;

    public static void SubstrateLive(float amount = 1.0f)
    {
        GD.Print($"PlayerState: SubstrateLive triggered. amount: {amount}");
        if (CurrentLives > 0)
        {
            CurrentLives -= amount;
        }
    }

    public static void ResetLives()
    {
        GD.Print("PlayerState: ResetLives triggered.");
        CurrentLives = 3;
    }
}
