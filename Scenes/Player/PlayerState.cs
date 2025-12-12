namespace Scenes.Player;

public static class PlayerState
{
    public static int CurrentLives { get; private set; } = 3;

    public static void SubstrateLive()
    {
        if (CurrentLives > 0)
        {
            CurrentLives--;
        }
    }
}
