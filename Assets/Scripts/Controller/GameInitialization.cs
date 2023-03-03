using UnityEngine;

internal sealed class GameInitialization
{
    public GameInitialization(Controllers controllers, Data data)
    {
        Camera camera = Camera.main;
        var playerFactory = new PlayerFactory(data.Player);
        var playerInitialization = new PlayerInitialization(playerFactory, data.Player.Position);
        if (camera != null)
            controllers.Add(new CameraController(playerInitialization.GetPlayer().transform, camera.transform));
    }
}