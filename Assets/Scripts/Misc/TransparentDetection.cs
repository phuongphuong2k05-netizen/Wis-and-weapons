using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transparencyAmount = 0.8f;
    [SerializeField] private float fadeTime = .4f;

    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    // Lưu lại Coroutine đang chạy để có thể dừng nó nếu người chơi ra/vào liên tục
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeInHierarchy) return;
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

            if (spriteRenderer)
            {
                fadeCoroutine = StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));
            }
            else if (tilemap)
            {
                fadeCoroutine = StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
            }
        }
    }

  
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!gameObject.activeInHierarchy) return;
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

            if (spriteRenderer)
            {
    
                fadeCoroutine = StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 1f));
            }
            else if (tilemap)
            {
                fadeCoroutine = StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, 1f));
            }
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }


        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, targetTransparency);
    }

    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }

        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, targetTransparency);
    }
}