using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject exp;
    public int xpDispertion = 3;
    public float lookRadius = 10f;
    public NavMeshAgent agent;

    private CharacterStats stats;
    private GameObject target;
    private Vector3 initPosition;
    private float initStoppingDist;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        agent = GetComponent<NavMeshAgent>();
        initPosition = transform.position;
        initStoppingDist = agent.stoppingDistance;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        FaceTarget();

        if (!target.GetComponent<PlayerController>().GetSafe())
        {
            //if (distance <= lookRadius)
            //{
                agent.SetDestination(target.transform.position);

            //}
            if (distance <= initStoppingDist)
                GetComponent<EnemyCombat>().canAttack = true;
            else
                GetComponent<EnemyCombat>().canAttack = false;
        }
        else
        {
            agent.SetDestination(initPosition);
        }

        if(stats.getLife() <= 0)
        {
            Die();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Die()
    {
        int xpos = 0;
        int zpos = 0;
        for(int i = 0; i < stats.experience/10 ; i++)
        {
            xpos = Random.Range(-xpDispertion, xpDispertion);
            zpos = Random.Range(-xpDispertion, xpDispertion);
            Instantiate(exp, new Vector3(xpos + transform.position.x, 0.75f, zpos + transform.position.z), Quaternion.identity);
        }

        Destroy(gameObject);
    }

    public float GetInitStoppingDist()
    {
        return initStoppingDist;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
