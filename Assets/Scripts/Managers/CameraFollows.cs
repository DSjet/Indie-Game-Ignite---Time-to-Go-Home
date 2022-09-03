using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(target.position.x, target.position.y, this.transform.position.z);
        transform.position = pos;
    }
}
