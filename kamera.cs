using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamera : MonoBehaviour
{
    public Transform player;

    public Vector3 pivot;

    void LateUpdate()
    {
        transform.position = player.position + pivot;
    }
}
