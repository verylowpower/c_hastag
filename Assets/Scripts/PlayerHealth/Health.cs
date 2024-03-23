using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float StartingtHealth;
    public float currentHealth { get; private set; }
    // Start is called before the first frame update
    void Start() // This method is called every frame
    {
    }

    private void Awake() // This method is only called once when the this script being call
    {
        currentHealth = StartingtHealth;
        print(currentHealth);
    }

    private void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, StartingtHealth);
        
        if (currentHealth > 0 )
        {
            //Player is being hurt
        }
        else
        {
            //Player died
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }
}
