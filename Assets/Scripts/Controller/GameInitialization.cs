using UnityEngine;

internal sealed class GameInitialization
{
    public GameInitialization(Controllers controllers, Data data)
    {
        Camera camera = Camera.main;
        var inputUserModel = new InputUserModel(data.InputData);
        var characterData = data.Character.GetCharacter(CharacterType.PoliceOfficer);
        //var inputInitialization = new InputInitialization();
        //var input = inputInitialization.GetInput();
        var playerFactory = new PlayerFactory(characterData);
        var playerInitialization = new PlayerInitialization(playerFactory, characterData.TransformSpawn.position);
        var supportObjectFactory = new SupportObjectFactory(data.SupportObject);
        var supportObjectInitialization = new SupportObjectInitialization(supportObjectFactory);
        var enemyFactory = new EnemyFactory(data.Enemy);
        var enemyInitialization = new EnemyInitialization(enemyFactory);
        var shotControllerInitialization = new ShotController();

        //controllers.Add(inputInitialization);
        controllers.Add(playerInitialization);
        controllers.Add(supportObjectInitialization);
        controllers.Add(enemyInitialization);
        controllers.Add(shotControllerInitialization);

        controllers.Add(new InputController(inputUserModel));
        controllers.Add(new CameraController(playerInitialization.GetPlayer().transform, inputUserModel, controllers));
        controllers.Add(new MoveController(inputUserModel, playerInitialization.GetPlayer().transform, characterData));
        controllers.Add(new ShootingController(inputUserModel, shotControllerInitialization, playerInitialization.GetPlayer()));
        controllers.Add(new EnemyMoveController(enemyInitialization.GetMoveEnemies(), playerInitialization.GetPlayer().transform));
    }
}