using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(Camera.main.transform);
    }
}
