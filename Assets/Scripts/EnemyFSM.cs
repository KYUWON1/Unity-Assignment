using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState { GoToBase,AttackBase,ChasePlayer,AttackPlayer,Patrol};
    public EnemyState currentState;

    public Sight sightSensor;
    public Transform baseTransform;
    public float baseAttackDistance;
    public float playerAttackDistance;
    public float lastShootTime;
    public float lastMoveTime;
    public float fireRate;
    public float moveRate;
    public GameObject bulletPrefab;
    public float moveSpeed;

    private NavMeshAgent agent;
    Vector3 moveTo;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 moveTo = new Vector3(Random.Range(-10,10),0,Random.Range(-10,10));
    }

    void Awake()
    {
        baseTransform = GameObject.Find("BaseDamagePoint").transform;
        agent = GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == EnemyState.GoToBase){GoToBase();}
        else if(currentState == EnemyState.AttackBase){AttackBase();}
        else if(currentState == EnemyState.ChasePlayer){ChasePlayer();}
        else if(currentState == EnemyState.Patrol){Patrol();}
        else {AttackPlayer();}
    }
    
    void Shoot()
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        if(timeSinceLastShoot > fireRate)
        {
            lastShootTime = Time.time;
            Instantiate(bulletPrefab,transform.position,Quaternion.LookRotation(transform.parent.right * -1));
        }
    }

    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.parent.position);
        directionToPosition.y =0;
        transform.parent.forward = directionToPosition;
    }

    void GoToBase()
    {
        agent.speed = moveSpeed;
        agent.isStopped = false;
        agent.SetDestination(baseTransform.position);
        //Debug.Log(baseTransform.position);

        if(sightSensor.detectObj != null && sightSensor.detectObj.CompareTag("Player")) // 아무것도없을때 .
        {
            currentState = EnemyState.ChasePlayer;
        }

        float distanceToBase = Vector3.Distance(transform.position,baseTransform.position);
        if(distanceToBase < baseAttackDistance)
        {
            currentState = EnemyState.AttackBase;
        }
        Debug.Log("GoToBase.");
    }

    void Patrol() //기능추가 
    {
        agent.isStopped = true;
        var timeSinceLastMove = Time.time - lastMoveTime;
        if(timeSinceLastMove > moveRate)
        {
            lastMoveTime = Time.time;
            moveTo = new Vector3(Random.Range(-10,10),0,Random.Range(-10,10));
            if(sightSensor.detectObj != null) // 아무것도없을때 .
            {
                moveTo *= -1;
            }
        }
        transform.parent.position += moveTo * Time.deltaTime * 1.5f;
        transform.parent.forward = moveTo;

        if(sightSensor.detectObj != null && sightSensor.detectObj.CompareTag("Player")) // 아무것도없을때 .
        {
            currentState = EnemyState.ChasePlayer;
        }
        float distanceToBase = Vector3.Distance(transform.position,baseTransform.position);
        if(distanceToBase < baseAttackDistance)
        {
            currentState = EnemyState.AttackBase;
        }
        Debug.Log("Patrol.");
    }

    void AttackBase()
    {
        agent.isStopped = true;
        LookTo(baseTransform.position);
        Shoot();
        Debug.Log("AttackBase.");
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        if(sightSensor.detectObj == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }
        agent.SetDestination(sightSensor.detectObj.transform.position);
        float distanceToPlayer = Vector3.Distance(transform.position,sightSensor.detectObj.transform.position);
        if(distanceToPlayer <= playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }
        Debug.Log("ChasePlayer.");
    }

    void AttackPlayer()
    {
        agent.isStopped = true;
         if(sightSensor.detectObj == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }
        LookTo(sightSensor.detectObj.transform.position);
        Shoot();
        float distanceToPlayer = Vector3.Distance(transform.position,sightSensor.detectObj.transform.position);
        if(distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = EnemyState.ChasePlayer;
        }
        Debug.Log("AttackPlayer.");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }
}
