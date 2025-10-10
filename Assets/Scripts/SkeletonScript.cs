using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] Transform[] point;
    int idxPoint = 0;
    float waitTime = 2f;
    private bool isWaiting = false;

    [SerializeField] float patrolSpeed;

    [Header("Chase Settings")]
    [SerializeField] Transform hero;
    [SerializeField] float chaseSpeed;
    bool isChasing = false;
    bool heroInRange = false;
    Animator anim;

    [Header("Attack Settings")]
    [SerializeField] float attackRate = 1f;
    private float nextAttackTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isJalan", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting || point.Length == 0) return;

        // logic untuk chasing dan patrol
        if (isChasing && heroInRange)
        {
            ChaseHero();
        }
        else
        {
            anim.SetBool("isJalan", true);
            Patrol();
        }
    }

    void Patrol()
    {
        Vector3 posTarget = new Vector3(point[idxPoint].position.x, transform.position.y, point[idxPoint].position.z);
        transform.position = Vector3.MoveTowards(transform.position, posTarget, patrolSpeed * Time.deltaTime);

        Vector3 arah = posTarget - transform.position;

        if (arah != Vector3.zero) transform.rotation = Quaternion.LookRotation(arah);

        if (Vector3.Distance(transform.position, posTarget) < 0.1f)
        {
            StartCoroutine(WaitAtPoint());
        }
    }

    IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        anim.SetBool("isJalan", false);

        yield return new WaitForSeconds(waitTime);

        idxPoint++;

        if (idxPoint >= point.Length) idxPoint = 0;

        isWaiting = false;
        anim.SetBool("isJalan", true);
    }

    void ChaseHero()
    {
        float distance = Vector3.Distance(transform.position, hero.position);

        if (distance <= 2f)
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }

            Vector3 arahAttack = hero.position - transform.position;
            arahAttack.y = 0;
            if (arahAttack != Vector3.zero) transform.rotation = Quaternion.LookRotation(arahAttack);

            return;
        }
        // if (Vector3.Distance(transform.position, hero.position) < 2f)
        // {
        //     Attack();
        //     return;
        // }

        Vector3 posHero = new Vector3(hero.position.x, transform.position.y, hero.position.z);
        transform.position = Vector3.MoveTowards(transform.position, posHero, chaseSpeed * Time.deltaTime);

        Vector3 arah = posHero - transform.position;
        if (arah != Vector3.zero) transform.rotation = Quaternion.LookRotation(arah);

        anim.SetFloat("speed", chaseSpeed);
        anim.SetBool("isJalan", true);
    }

    private void Attack()
    {
        anim.SetBool("isJalan", false);
        anim.SetTrigger("isSerang");
    }

    public void DealDamage()
    {
        if (Vector3.Distance(transform.position, hero.position) <= 2f)
        {
            hero.GetComponent<LadyPirateScript>()?.TakeDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            heroInRange = true;
            isChasing = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            heroInRange = true;
            isChasing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            heroInRange = false;
            isChasing = false;
        }
    }
}
