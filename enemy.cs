using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    //parameter
    public int maxHealth;
    int currHealth;
    public int attackDamage;

    public Transform player;

    public Transform attackRange;
    public float attackRadius;
    public LayerMask playerLayer;

    //weapon
    public GameObject bomb;

    //component
    Animator anim;
    SpriteRenderer renderer;
    Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
    }

    void Update()
    {
        //check distance between pig and player
        float dist = (transform.position - player.position).sqrMagnitude;
        
        //give a cooldown to attack
        float cooldown = 0;
        float timeToAttack = 3f;
        cooldown -= Time.deltaTime;

        if(dist <= 10f && dist >= .5f)
        {   
            ChasePlayer();
           
            if(dist <= 2f && cooldown <= 0)
            {
                anim.SetTrigger("attack");
                cooldown = timeToAttack;
            }
        }
    }

    void ChasePlayer()
    {
        //start chasing player
            transform.position = Vector2.MoveTowards(transform.position , player.position , 1.5f * Time.deltaTime);

            anim.SetFloat("run", rb.velocity.magnitude);
            
            //look at the player
            if(transform.position.x <= player.position.x)
            {
                renderer.flipX = true;
            }
            else if(transform.position.x >= player.position.x)
            {
                renderer.flipX = false;
            }
    }

    public void PigAttack()
    {
        
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackRange.position , attackRadius , playerLayer);

        foreach (Collider2D player in playerCollider)
        {
            player.GetComponent<player>().TakeDamage(attackDamage);
        }  
    }

    public void ThrowBomb()
    {
        GameObject bombPrefabs = Instantiate(bomb,transform.position, transform.rotation);
        bombPrefabs.GetComponent<Rigidbody2D>().AddForce(transform.right * 10f);
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;

        anim.SetTrigger("hit");

        if(currHealth <= 0)
        Die();
    }

    void Die()
    {
        anim.SetBool("dead",true);
        Destroy(gameObject , 1.5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackRange.position , attackRadius);
    }
}
