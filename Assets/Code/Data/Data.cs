﻿using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
public sealed class Data : ScriptableObject
{
    [SerializeField] private string _playerDataPath;
    [SerializeField] private string _enemyDataPath;
    //[SerializeField] private string _interactiveObjectDataPath;
    [SerializeField] private string _cursorDataPath;
    [SerializeField] private string _supportObjectDataPath;
    [SerializeField] private string _inputDataPath;
    
    
    private CharacterDatas _character;
    private EnemyData _enemy;
    //private InteractiveObjectData _interactiveObject;
    private CursorData _cursor;
    private SupportObjectData _supportObject;
    private InputData _inputData;
    

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

    public CursorData Cursor
    {
        get
        {
            if (_cursor == null)
            {
                _cursor = Load<CursorData>("Data/" + _cursorDataPath);
            }

            return _cursor;
        }
    }
    
    public SupportObjectData SupportObject
    {
        get
        {
            if (_supportObject == null)
            {
                _supportObject = Load<SupportObjectData>("Data/" + _supportObjectDataPath);
            }

            return _supportObject;
        }
    }
    
    public InputData InputData
    {
        get
        {
            if (_inputData == null)
            {
                _inputData = Load<InputData>("Data/" + _inputDataPath);
            }

            return _inputData;
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