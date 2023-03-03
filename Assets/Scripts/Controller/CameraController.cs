using UnityEngine;

internal sealed class CameraController : ILateExecute
{
    private readonly Transform _player;
    private readonly Transform _mainCamera;
    private readonly Vector3 _offset;
    
    
    public CameraController(Transform player, Transform mainCamera)
    {
        _player = player;
        _mainCamera = mainCamera;
        //_player = FindObjectOfType<Player>();
        _offset = _mainCamera.transform.position - player.transform.position;
    }

    public void LateExecute(float deltaTime)
    {
        _mainCamera.transform.position = _player.transform.position + _offset;
    }
}
