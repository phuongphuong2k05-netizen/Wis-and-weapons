using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 5f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;
    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
   
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

        if(enemy != null && canTakeDamage)
        {
            TakeDamage(1);
            knockBack.GetKnockedBack(collision.gameObject.transform,knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
        }

    }
    private void TakeDamage(int damage)
    {
     canTakeDamage = false;
        currentHealth -= damage;
        StartCoroutine(DamageRecoveryRoutine());

    
    }
    private IEnumerator DamageRecoveryRoutine()
    {
               yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
}

