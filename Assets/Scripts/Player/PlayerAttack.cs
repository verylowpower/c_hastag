using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform flamePoint;
    [SerializeField] private GameObject[] flameBalls;
    private float coolDownTimer = Mathf.Infinity;
    private Animator MyAnimator;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        MyAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && coolDownTimer > attackCoolDown && playerMovement.canAttack())
        {
            Attack();
            print("Key is being press");
            
        }
        coolDownTimer += Time.deltaTime;
        //coolDownTimer = coolDownTimer + Time.deltaTime;
    }

    private void Attack()
    {
        MyAnimator.SetTrigger("attackFar");
        coolDownTimer = 0;
        flameBalls[findBall()].transform.position = flamePoint.position;
        flameBalls[findBall()].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }

    private int findBall()
    {
        for (int i = 0; i< flameBalls.Length; i++)
        {
            if (!flameBalls[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}



