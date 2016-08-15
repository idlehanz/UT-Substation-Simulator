/*script to control squirrel movement*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SquirrelMovement:MonoBehaviour
{
    Transform player;
    NavMeshAgent navMesh;
    Animator animator;

    void Start()
    {
        //get the player transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //get teh navmesh
        navMesh = GetComponent<NavMeshAgent>();
        //get the animator
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //get the distance from the player to the squirrel.
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <=8)
        {
            //if the player is within distance, set IsMOving to false andset the destination to the current position
            animator.SetBool("IsMoving", false);
            navMesh.SetDestination(transform.position);
        }
        else
        {

            //if the player isn't within distance, set ismoving to true and set teh destination to the player position.
            animator.SetBool("IsMoving", true);
            navMesh.SetDestination(player.position);
        }

    }

   
}

