using UnityEngine;

internal sealed class CameraController : ILateExecute
{
    private readonly Transform _player;
    private readonly Camera _camera;
    private readonly Vector3 _offset;
    
    
    public CameraController(Transform player, InputUserModel inputModel, Controllers controllers)
    {
        _player = player;
        _camera = new GameObject().AddComponent<Camera>();
        _camera.transform.position = new Vector3(0.0f, 4.93f, -5.61f);
        _camera.transform.rotation = Quaternion.Euler(30.0f, 0.0f,0.0f);
        _offset = _camera.transform.position - player.transform.position;
        
        controllers.Add(new CursorController(_camera, inputModel));
    }

    public void LateExecute(float deltaTime)
    {
        _camera.transform.position = _player.transform.position + _offset;
    }
}
