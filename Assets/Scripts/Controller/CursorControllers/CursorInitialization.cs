using UnityEngine;

public class CursorInitialization : IInitialization
{
    private readonly ICursorFactory _cursorFactory;
    private Transform _cursor;

    public CursorInitialization(ICursorFactory cursorFactory, Vector3 positionCursor)
    {
        _cursorFactory = cursorFactory;
        _cursor = _cursorFactory.CreateCursor();
        _cursor.position = positionCursor;
    }

    public void Initialization()
    {
    }

    public Transform GetCursor()
    {
        return _cursor;
    }
}