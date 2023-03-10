using UnityEngine;

internal sealed class GameInitialization
{
    public GameInitialization(Controllers controllers, Data data)
    {
        Camera camera = Camera.main;
        var characterData = data.Character.GetCharacter(CharacterType.PoliceOfficer);
        var inputInitialization = new InputInitialization();
        var input = inputInitialization.GetInput();
        var playerFactory = new PlayerFactory(characterData);
        var playerInitialization = new PlayerInitialization(playerFactory, characterData.TransformSpawn.position);
        var supportObjectFactory = new SupportObjectFactory(data.SupportObject);
        var supportObjectInitialization = new SupportObjectInitialization(supportObjectFactory);
        var enemyFactory = new EnemyFactory(data.Enemy);
        var enemyInitialization = new EnemyInitialization(enemyFactory);
        var shotControllerInitialization = new ShotController();

        controllers.Add(inputInitialization);
        controllers.Add(playerInitialization);
        controllers.Add(supportObjectInitialization);
        controllers.Add(enemyInitialization);
        controllers.Add(shotControllerInitialization);

        controllers.Add(new InputController(inputInitialization.GetInput()));
        controllers.Add(new CursorController(camera, input.inputMousePosition));
        controllers.Add(new MoveController((input.inputHorizontal, input.inputVertical, input.inputRotation), playerInitialization.GetPlayer().transform, characterData));
        controllers.Add(new ShootingController(input.pcInputFire, shotControllerInitialization, playerInitialization.GetPlayer()));
        controllers.Add(new EnemyMoveController(enemyInitialization.GetMoveEnemies(), playerInitialization.GetPlayer().transform));
        if (camera != null) controllers.Add(new CameraController(playerInitialization.GetPlayer().transform, camera.transform));
    }
}