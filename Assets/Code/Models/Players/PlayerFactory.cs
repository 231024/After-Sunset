using UnityEngine;

public sealed class PlayerFactory : IPlayerFactory
{
    private readonly CharacterData _characterData;

    public PlayerFactory(CharacterData characterData)
    {
        _characterData = characterData;
    }

    public Transform CreatePlayer()
    {
        return new GameObject(GameConstants.PLAYER).AddUnit(_characterData).
            AddRigidbody(_characterData.Mass, _characterData.AngularDrag, _characterData.IsGravity, _characterData.IsFreeze).transform;
    }
}