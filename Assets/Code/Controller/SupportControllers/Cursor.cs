using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Cursor : SupportObjectProvider, IEquatable<Cursor>
{
    private int _cursor;
    
    public bool Equals(Cursor other)
    {
        return _cursor == other._cursor;
    }
}