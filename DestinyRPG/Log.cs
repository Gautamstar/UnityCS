﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    private Rigidbody2D myRigidBody;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currenState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius) 
        {
            if (currenState == EnemyState.idle || currenState == EnemyState.walk && currenState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidBody.MovePosition(temp);
                changeState(EnemyState.walk);
                anim.SetBool("wakeUp", true); 
            }
        } else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
        }
    }

    private void setAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    private void changeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                setAnimFloat(Vector2.right);

            }else if(direction.x < 0)
            {
                setAnimFloat(Vector2.left);
            }
            
            
        }else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if(direction.y > 0)
            {
                setAnimFloat(Vector2.up);

            }
            else if(direction.y < 0)
            {
                setAnimFloat(Vector2.down);
            }
        }
    }

    private void changeState(EnemyState newState)
    {
        if(currenState != newState)
        {
            currenState = newState;
        }
    }
}
