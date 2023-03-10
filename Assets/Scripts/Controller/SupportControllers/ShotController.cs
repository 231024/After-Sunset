using UnityEngine;

public sealed class ShotController : IInitialization, IFixedExecute
{
    private readonly LineRenderer _lineRenderer;
    private GameObject _gameObject;
    private bool _visible;

    public ShotController()
    {
        _gameObject = GameObject.Find(SupportObjectType.Shot.ToString());
        _lineRenderer = _gameObject.GetComponent<LineRenderer>();
    }

    public void Initialization()
    {
    }

    public void FixedExecute(float deltaTime)
    {
        if (_visible)
            _visible = false;
        else
            _gameObject.SetActive(false);
    }

    public void Show(Vector3 from, Vector3 to)
    {
        _lineRenderer.SetPositions(new Vector3[]{ from, to });
        _visible = true;
        _gameObject.SetActive(true);
    }
}