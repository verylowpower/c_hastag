using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float fireBallSpeed;
    private Animator myAnimator;
    private BoxCollider2D MyBoxCollider2D;
    private float lifeTime;
    private bool isHit;
    private float direction;
    private void Awake()
    {
        MyBoxCollider2D = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if(isHit) { return; }
        float FlameBallMovement = fireBallSpeed * Time.deltaTime * direction;
        transform.Translate(FlameBallMovement,0,0);

        lifeTime += Time.deltaTime;
        if (lifeTime > 5) 
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHit = true;
        MyBoxCollider2D.enabled = false;
        myAnimator.SetTrigger("Explosion");
        
    }
    public void setDirection(float _direction)
    {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        isHit = false;
        MyBoxCollider2D.enabled = true;

        float localScale = transform.localScale.x;
        if (Mathf.Sign(localScale) != direction)
            transform.localScale = new Vector2(direction, transform.localScale.y);
    }

    private void DeactiveFlameBall()
    {
        gameObject.SetActive(false);
    }
}
