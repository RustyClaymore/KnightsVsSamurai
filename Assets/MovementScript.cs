using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour
{

    public Transform targetTransform;
    NavMeshAgent agent;

    public GameObject[] enemies;
    List<GameObject> enemiesToFleeFrom;
    bool isFleeing = false;

    Vector3 fleeingDestination;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesToFleeFrom = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < 3)
            {
                enemiesToFleeFrom.Add(enemy);
                isFleeing = true;
            }
        }

        if (isFleeing)
        {
            fleeingDestination = Vector3.zero;
            for (int i = 0; i < enemiesToFleeFrom.Count; i++)
            {
                fleeingDestination += enemiesToFleeFrom[i].transform.position;
            }
            fleeingDestination = ((transform.position - (fleeingDestination / enemiesToFleeFrom.Count)).normalized + targetTransform.position.normalized) * 5f;

            agent.SetDestination(fleeingDestination);
        }
        else
        {
            agent.SetDestination(targetTransform.position);
        }

        isFleeing = false;
        enemiesToFleeFrom = new List<GameObject>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(fleeingDestination, 0.5f);
    }
}
