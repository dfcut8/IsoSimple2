using Godot;
using System.Linq;

namespace Scenes.Managers;

public partial class CameraManager : Node2D
{
    override public void _Ready()
    {
        var followCamera = new Camera2D();
        followCamera.Zoom = new Vector2(4f, 4f);
        var groundTileMap = GetTree().GetNodesInGroup("Ground").Cast<TileMapLayer>().FirstOrDefault();
        // Fix for CS0019: Operator '*' cannot be applied to operands of type 'Rect2I' and 'Vector2I'
        // Instead, calculate the map size by multiplying the size of the used rect by the tile size.
        var usedRect = groundTileMap.GetUsedRect();
        var tileSize = groundTileMap.TileSet.TileSize;
        var mapSize = new Rect2I(
            usedRect.Position * tileSize,
            usedRect.Size * tileSize
        );
        followCamera.LimitEnabled = false;
        followCamera.LimitBottom = (mapSize.End.Y);
        followCamera.LimitRight = (mapSize.End.X);
        followCamera.LimitTop = (mapSize.Position.Y);
        followCamera.LimitLeft = (mapSize.Position.X);
        CharacterBody2D player = GetTree().GetNodesInGroup("Player").Cast<CharacterBody2D>().FirstOrDefault();
        player.AddChild(followCamera);
    }
}
