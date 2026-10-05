using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private KnockBack knockBack;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if(knockBack.gettingKnockBack) return;
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
        if(moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    public void MoveTo(Vector2 direction)
    {
        moveDir = direction;
    }
}
