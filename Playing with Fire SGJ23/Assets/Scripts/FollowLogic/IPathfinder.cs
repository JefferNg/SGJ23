using UnityEngine;

interface IPathfinder
{
    public Vector2 NextPoint(Vector2 currentPosition);
    public void Initialize();
}
