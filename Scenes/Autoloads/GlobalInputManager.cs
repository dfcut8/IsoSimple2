using Godot;
using Scenes.Players;

namespace Scenes.Autoloads;

public partial class GlobalInputManager : Node
{
    public const string GLOBAL_INPUT_STAGE_RESET = "global_stage_reset";

    public static GlobalInputManager Instance { get; private set; }

    public GlobalInputManager()
    {
        Instance = this;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(GLOBAL_INPUT_STAGE_RESET))
        {
            GetTree().ReloadCurrentScene();
            PlayerState.ResetLives();
        }
    }
}
