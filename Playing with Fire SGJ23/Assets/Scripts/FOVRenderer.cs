using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVRenderer : MonoBehaviour
{
    public void Initialize(float radius, float angle, Vector2 dirCCW, Vector2 dirCW)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triIndeces = new List<int>();

        float doubleAngle = 2 * angle;


        int numArcVerts = Mathf.RoundToInt(Mathf.Lerp(3, 20, (angle / 180.0f)));
        for (int i = 0; i < numArcVerts; i++)
        {
            float t = (float)i / (numArcVerts - 1);
            float rotationAngle = Mathf.Lerp(0, doubleAngle, t);
            Vector2 edge = Quaternion.AngleAxis(rotationAngle, Vector3.back) * dirCCW;
            vertices.Add(edge.normalized * radius);
            uvs.Add(new Vector2(1.0f, 1.0f));
        }

        vertices.Add(new Vector3(0.0f, 0.0f, 0.0f));
        uvs.Add(new Vector2(0.0f, 0.0f));

        int centerIndex = vertices.Count - 1;

        for (int i = 0; i < numArcVerts - 1; i++)
        {
            triIndeces.Add(i);
            triIndeces.Add(centerIndex);
            triIndeces.Add(i + 1);
        }

        MeshFilter filter = GetComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = triIndeces.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        filter.mesh = mesh;

    }
}
