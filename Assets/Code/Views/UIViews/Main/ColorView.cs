using UnityEngine;


public class ColorView : MonoBehaviour
{
    [Header("StatusColor")] 
    [SerializeField] private Color _colorLoading;
    [SerializeField] private Color _colorSuccess;
    [SerializeField] private Color _colorFailure;

    public Color ColorLoading => _colorLoading;
    public Color ColorSuccess => _colorSuccess;
    public Color ColorFailure => _colorFailure;
}