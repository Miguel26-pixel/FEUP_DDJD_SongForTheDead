using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class MiniGhost : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public bool isAlive;
    public bool isToAttack = false;

    public bool shouldRotate;
    public bool destroy = false;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;
    public float Health {
        set {
            _health = value;

            if(_health <= 0) {
                anim.SetTrigger("Death");
                anim.SetBool("Destroy", true);
                destroy = true;

            }
        }
        get{
            return _health;
        }
    }

    public float _health = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        anim.SetBool("isAlive", true);
    }

    private void Update()
    {

        anim.SetBool("isRunning", isInChaseRange);

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if(shouldRotate)
        {
            anim.SetFloat("x", dir.x);
            anim.SetFloat("y", dir.y);
        }
    }

    private void FixedUpdate()
    {

        if(isInChaseRange && !isInAttackRange)
        {
            anim.SetBool("isToAttack", false);
            MoveCharacter(movement);
        }
        if(isInAttackRange)
        {
            anim.SetBool("isToAttack", true);
        }
        if(destroy){
            Destroy(gameObject);
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir*speed*Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("ola");
        Health -= 1;
    }

}