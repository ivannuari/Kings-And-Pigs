using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig_canon : MonoBehaviour
{
    public GameObject canonBall;
    public Transform canon;
    public bool canShoot = false;

    public float requestTime = 2f;
    float cooldown;

    Animator[] anim;

    void Start()
    {
        anim = GetComponentsInChildren<Animator>();
        cooldown = requestTime;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            canShoot = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            canShoot = false;
        }
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if(canShoot && cooldown <= 0)
        {
            foreach (Animator a in anim)
            {
                a.SetTrigger("shoot");
            }
            cooldown = requestTime;
        }
    }

    public void CanonShoot()
    {
        GameObject ball = Instantiate(canonBall , canon.position , canon.rotation);
    }
}
