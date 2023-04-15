using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseWorldCenterMode : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Marker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var matrix4x4 = Marker.transform.localToWorldMatrix;
        matrix4x4 = matrix4x4.inverse;
        Camera.transform.position = matrix4x4.GetPosition();
        Camera.transform.rotation = matrix4x4.rotation;
        Marker.transform.position = Vector3.zero;
        Marker.transform.eulerAngles = Vector3.zero;
    }
}
