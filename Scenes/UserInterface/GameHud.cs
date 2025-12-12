using System.Linq;
using Godot;
using Scenes.Players;

namespace Scenes.UserInterface;

public partial class GameHud : CanvasLayer
{
    private readonly int heartsRowSize = 8;
    private readonly int heartsOffset = 16;
    private Sprite2D heartsSprite;

    public override void _Ready()
    {
        heartsSprite = GetNode<Sprite2D>("%Hearts");
        for (int i = 0; i < PlayerState.CurrentLives; i++)
        {
            var sprite = new Sprite2D();
            sprite.Texture = heartsSprite.Texture;
            sprite.Hframes = heartsSprite.Hframes;
            heartsSprite.AddChild(sprite);
        }
    }

    public override void _Process(double delta)
    {
        foreach (var heart in heartsSprite.GetChildren().Cast<Sprite2D>())
        {
            var i = heart.GetIndex();
            var x = i % heartsRowSize * heartsOffset;
            var y = i / heartsRowSize * heartsOffset;
            heart.Position = new Vector2(x, y);
            var lastFullHeart = Mathf.Floor(PlayerState.CurrentLives);
            if (i > lastFullHeart)
            {
                heart.Frame = 0;
            }
            else if (i == lastFullHeart)
            {
                var f = (int)((PlayerState.CurrentLives - lastFullHeart) * 4);
                heart.Frame = f;
            }
            else
            {
                heart.Frame = 4;
            }
        }
    }
}
