using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonBall : MonoBehaviour
{
    public float powerForce;

    void Update()
    {
        transform.Translate(Vector2.right * powerForce * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if(col.CompareTag("platforms"))
        {
            Destroy(gameObject);
        }
    }
}
