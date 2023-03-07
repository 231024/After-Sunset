using UnityEngine;

public class CursorController : IExecute, ICleanup
{
    private readonly Camera _camera;
    private readonly IUserInputProxy<Vector3> _pcInputMousePosition;
    private Transform _cursor;
    private Vector3 _mousePosition;
    private SpriteRenderer _spriteRenderer;
    private int _layerMask;
    
    public CursorController(Camera mainCamera, 
        IUserInputProxy<Vector3> inputMousePosition, Transform cursor)
    {
        _camera = mainCamera;
        _pcInputMousePosition = inputMousePosition;
        _cursor = cursor;
        
        _pcInputMousePosition.AxisOnChange += MousePositionOnAxisOnChange;
        _spriteRenderer = _cursor.GetComponentInParent<SpriteRenderer>();
        _layerMask = LayerMask.GetMask("Ground");
    }
    
    private void MousePositionOnAxisOnChange(Vector3 value)
    {
        _mousePosition = value;
    }

    public void Execute(float deltaTime)
    {
        var ray = _camera.ScreenPointToRay(_mousePosition);

        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 1000, _layerMask))
            _spriteRenderer.enabled = false;
        else
        {
            _cursor.position = new Vector3(hit.point.x, _cursor.position.y, hit.point.z);
            _spriteRenderer.enabled = true;
        }
    }

    public void Cleanup()
    {
        _pcInputMousePosition.AxisOnChange -= MousePositionOnAxisOnChange;
    }
}