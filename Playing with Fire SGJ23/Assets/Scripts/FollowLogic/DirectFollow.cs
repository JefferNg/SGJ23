using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectFollow : IPathfinder
{
    private Transform _playerTransform = null;

    public void Initialize()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = player.transform;
    }

    public Vector2 NextPoint(Vector2 currentPosition)
    {
        return _playerTransform.position;
    }

}
