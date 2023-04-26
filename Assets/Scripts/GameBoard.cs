using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    private EdgeCollider2D _edgeCollider;
    private Vector2[] _edgeColliderWorldPoints;


    private void Start()
    {
        _edgeCollider = GetComponent<EdgeCollider2D>();
        _edgeColliderWorldPoints = new Vector2[_edgeCollider.pointCount];

        for (int i = 0; i < _edgeCollider.pointCount; i++)
        {
            _edgeColliderWorldPoints[i] = _edgeCollider.transform.TransformPoint(_edgeCollider.points[i]);
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public Vector2 GetMinCorner()
    {
        var bottomLeftCorner = Vector2.positiveInfinity;

        foreach (var point in _edgeColliderWorldPoints)
        {
            if (point.x < bottomLeftCorner.x && point.y < bottomLeftCorner.y)
            {
                bottomLeftCorner = point;
            }
        }

        return bottomLeftCorner;
    }

    public Vector2 GetMaxCorner()
    {
        var topRightCorner = Vector2.negativeInfinity;

        foreach (var point in _edgeColliderWorldPoints)
        {
            if (point.x > topRightCorner.x && point.y > topRightCorner.y)
            {
                topRightCorner = point;
            }
        }

        return topRightCorner;
    }
}
