using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera camera;

    void Awake()
    {
        camera = Camera.main;
    }


    void Update()
    {
        transform.LookAt(camera.transform.position);
        Quaternion rot = transform.rotation;
        rot.x = 0;
        rot.z = 0;
        transform.rotation = rot;
    }
}
