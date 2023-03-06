﻿using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
public sealed class Data : ScriptableObject
{
    [SerializeField] private string _playerDataPath;
    [SerializeField] private string _enemyDataPath;
    //[SerializeField] private string _interactiveObjectDataPath;
    private CharacterDatas _character;
    private EnemyData _enemy;
    //private InteractiveObjectData _interactiveObject;
    

    public CharacterDatas Character
    {
        get
        {
            if (_character == null)
            {
                _character = Load<CharacterDatas>("Data/" + _playerDataPath);
            }

            return _character;
        }
    }
    

    public EnemyData Enemy
    {
        get
        {
            if (_enemy == null)
            {
                _enemy = Load<EnemyData>("Data/" + _enemyDataPath);
            }

            return _enemy;
        }
    }
        
    // public InteractiveObjectData InteractiveObject
    // {
    //     get
    //     {
    //         if (_interactiveObject == null)
    //         {
    //             _interactiveObject = Load<InteractiveObjectData>("Data/" + _interactiveObjectDataPath);
    //         }
    //
    //         return _interactiveObject;
    //     }
    // }
        
    private T Load<T>(string resourcesPath) where T : Object =>
        Resources.Load<T>(Path.ChangeExtension(resourcesPath, null));
}