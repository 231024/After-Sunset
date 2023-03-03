using UnityEngine;

public sealed class PlayerFactory : IPlayerFactory
{
    private readonly PlayerData _playerData;

    public PlayerFactory(PlayerData playerData)
    {
        _playerData = playerData;
    }

    public Transform CreatePlayer()
    {
        return new GameObject("Player").AddUnit(_playerData).
            AddRigidbody(_playerData.Mass, _playerData.AngularDrag, _playerData.IsGravity, _playerData.IsFreeze).transform;
    }
}