using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
   public bool gettingKnockBack { get; private set; }
    [SerializeField] private float knockBackTime = .2f;
    private Rigidbody2D rb; 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GetKnockedBack(Transform damageSource , float knockBackForce)
    {
        gettingKnockBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackForce * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());

    }
    public IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.linearVelocity = Vector2.zero;
        gettingKnockBack = false;
    }
}
