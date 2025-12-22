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
        var topLeft = groundTileMap.MapToLocal(usedRect.Position);
        GD.Print(topLeft); // (112, -8)
        var bottomRight = groundTileMap.MapToLocal(usedRect.End);
        GD.Print(bottomRight); // (48, 152)
        var size = groundTileMap.MapToLocal(usedRect.Size);
        GD.Print(size); // (-48, 168)


        followCamera.LimitEnabled = true;
        followCamera.LimitBottom = (int)size.Y;
        followCamera.LimitRight = (int)size.X;
        followCamera.LimitTop = (int)topLeft.Y;
        //followCamera.LimitLeft = -((int)topLeft.X + groundTileMap.TileSet.TileSize.X);
        followCamera.LimitLeft = -(int)topLeft.X / 2;
        CharacterBody2D player = GetTree().GetNodesInGroup("Player").Cast<CharacterBody2D>().FirstOrDefault();
        player.AddChild(followCamera);
    }
}
