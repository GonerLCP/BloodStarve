using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform[] _points;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        SetUpLine(_points);
    }

    public void SetUpLine( Transform[] points)
    {
        _lineRenderer.positionCount = _points.Length;
        this._points = points;
    }

    private void Update()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            var pointZNull = _points[i].position;
            pointZNull.z = 0; 
            _lineRenderer.SetPosition(i,pointZNull);
        }
    }
}
