using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "CharacterSettings", menuName = "Data/Unit/CharacterSettings")]
public class CharacterDatas : ScriptableObject
{
    [SerializeField] private List<CharacterInfo> _playerInfos;

    public List<CharacterInfo> ListPlayerInfos => _playerInfos;

    [Serializable] 
    public struct CharacterInfo
    {
        public CharacterType Type;
        public CharacterData characterInfo;
    }

    public CharacterData GetCharacter(CharacterType typeCharacterType)
    {
        var characterInfo = _playerInfos.First(info => info.Type == typeCharacterType);
        return characterInfo.characterInfo;
    }
}
