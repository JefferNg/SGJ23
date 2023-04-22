using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView: MonoBehaviour
{
    [SerializeField]
    private float _radius = 2.0f;

    [SerializeField]
    private float _angle = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetRadius()
    {
        return _radius;
    }

    public float GetAngle()
    {
        return _angle;
    }

    public void GetViewDirections(out Vector2 dirA, out Vector2 dirB)
    {
        float zRotation = transform.rotation.eulerAngles.z;
        Quaternion localToGlobalRot = Quaternion.AngleAxis(zRotation, Vector3.forward);
        Vector2 globalUp = localToGlobalRot * Vector2.up;

        Quaternion fovRotA = Quaternion.AngleAxis(_angle, Vector3.forward);
        Quaternion fovRotB = Quaternion.AngleAxis(-_angle, Vector3.forward);
        dirA = fovRotA * globalUp;
        dirB = fovRotB * globalUp;
    }
}
