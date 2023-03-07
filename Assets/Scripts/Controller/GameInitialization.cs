﻿using UnityEngine;

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
        var cursorFactory = new CursorFactory(data.Cursor);
        var cursorInitialization = new CursorInitialization(cursorFactory, Vector3.zero);
        var enemyFactory = new EnemyFactory(data.Enemy);
        var enemyInitialization = new EnemyInitialization(enemyFactory);

        controllers.Add(inputInitialization);
        controllers.Add(playerInitialization);
        controllers.Add(cursorInitialization);
        controllers.Add(enemyInitialization);

        controllers.Add(new InputController(inputInitialization.GetInput()));
        controllers.Add(new CursorController(camera, input.inputMousePosition, cursorInitialization.GetCursor()));
        controllers.Add(new MoveController((input.inputHorizontal, input.inputVertical, input.inputRotation), playerInitialization.GetPlayer().transform, characterData, cursorInitialization.GetCursor()));
        controllers.Add(new EnemyMoveController(enemyInitialization.GetMoveEnemies(), playerInitialization.GetPlayer().transform));
        if (camera != null) controllers.Add(new CameraController(playerInitialization.GetPlayer().transform, camera.transform));
    }
}