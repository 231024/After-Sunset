using UnityEngine;

public sealed class CursorFactory : ICursorFactory
{
    private readonly CursorData _cursorData;
    private ICursor _cursor;

    public CursorFactory(CursorData cursorData)
    {
        _cursorData = cursorData;
    }
    
    public Transform CreateCursor()
    {
        return new GameObject(GameConstants.CURSOR).AddCursor(_cursorData).transform;
    }
}