using UnityEngine;

internal sealed class GameInitialization
{
    public GameInitialization(Controllers controllers, Data data)
    {
        Camera camera = Camera.main;
        var characterData = data.Character.GetCharacter(CharacterType.PoliceOfficer);
        var inputInitialization = new InputInitialization();
        var playerFactory = new PlayerFactory(characterData);
        var playerInitialization = new PlayerInitialization(playerFactory, characterData.TransformSpawn.position);
        var enemyFactory = new EnemyFactory(data.Enemy);
        var enemyInitialization = new EnemyInitialization(enemyFactory);

        controllers.Add(inputInitialization);
        controllers.Add(playerInitialization);
        controllers.Add(enemyInitialization);

        controllers.Add(new InputController(inputInitialization.GetInput()));
        controllers.Add(new MoveController(inputInitialization.GetInput(), playerInitialization.GetPlayer().transform, characterData));
        controllers.Add(new EnemyMoveController(enemyInitialization.GetMoveEnemies(), playerInitialization.GetPlayer().transform));
        if (camera != null) controllers.Add(new CameraController(playerInitialization.GetPlayer().transform, camera.transform));
    }
}