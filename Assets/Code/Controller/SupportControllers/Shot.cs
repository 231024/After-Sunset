using System;

public class Shot : SupportObjectProvider, IEquatable<Shot>
{
    private int _lineRenderer;


    public bool Equals(Shot other)
    {
        return _lineRenderer == other._lineRenderer;
    }
}