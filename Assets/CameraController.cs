using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform ragdoll;
    public Vector3 offset;
    public Vector3 rotation;

    public Transform toFollow;

    void Start()
    {
        toFollow = player;
        transform.rotation = Quaternion.Euler(rotation);
    }

    void Update()
    {
        transform.position = new Vector3(toFollow.position.x + offset.x, offset.y, toFollow.position.z + offset.z);
    }

    public void CameraDie()
    {
        toFollow = ragdoll;
    }

    public void CameraReset()
    {
        toFollow = player;
    }
}
