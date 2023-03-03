using UnityEngine;

internal sealed class GameInitialization
{
    public GameInitialization(Controllers controllers, Data data)
    {
        Camera camera = Camera.main;
        var characterData = data.Character.GetCharacter(CharacterType.PoliceOfficer);
        var playerFactory = new PlayerFactory(characterData);
        var playerInitialization = new PlayerInitialization(playerFactory, characterData.TransformSpawn.position);
        if (camera != null)
            controllers.Add(new CameraController(playerInitialization.GetPlayer().transform, camera.transform));
    }
}