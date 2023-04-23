using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelManager : MonoBehaviour
{
    public Intel intel;
    // Start is called before the first frame update

    public Vector2[] locations = null;
    void Start()
    {
        if (locations != null) {
            intel.transform.position = locations[Random.Range(0, locations.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
