using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfView)), CanEditMultipleObjects]
public class FOVEditor : Editor
{
    public void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        float radius = fov.GetRadius();
        float angle = fov.GetAngle();

        Vector3 center = fov.transform.position;
        Handles.DrawWireDisc(center, Vector3.back, radius);

        Vector2 viewA = Vector2.zero;
        Vector2 viewB = Vector2.zero;

        fov.GetViewDirections(out viewA, out viewB);

        Handles.color = Color.red;
        Handles.DrawLine(center, center + (Vector3)viewA * radius);

        Handles.color = Color.blue;
        Handles.DrawLine(center, center + (Vector3)viewB * radius);

        Handles.color = Color.green;
        foreach (Vector2 fovTargetPos in fov.GetTargetPositionsEnumerator())
        {
            Handles.DrawLine(center, fovTargetPos);
        }

        //Handles.DrawSolidArc(center, Vector3.back, viewA, 2 * angle, radius);
    }
}
