using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public GameObject target;
    public float x, y;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;
        transform.localPosition += new Vector3(-x, y, -x);
        transform.LookAt(target.transform);
    }
}
