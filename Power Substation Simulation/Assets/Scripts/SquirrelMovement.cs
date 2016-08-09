using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SquirrelMovement:MonoBehaviour
{
    Transform player;
    NavMeshAgent navMesh;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        navMesh = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        navMesh.SetDestination(player.position);

    }
}

