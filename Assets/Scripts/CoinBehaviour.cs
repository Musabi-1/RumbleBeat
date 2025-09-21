using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 6f;
    [SerializeField] private float dyingAnimationDuration = 1.2f;
    [SerializeField] private float lifeSpan = 8f;

    private float targetY;
    private bool reachedTarget = false;
    private Animator coinAnim;
    private float lifeCountDown = 0;

    private void Awake()
    {
        coinAnim = GetComponentInChildren<Animator>();
    }

    public void SetTargetY(float y)
    {
        targetY = y;
    }

    private void Update()
    {
        if (reachedTarget)
        {
            if (lifeCountDown < lifeSpan)
            {
                lifeCountDown += Time.deltaTime;
            }
            else
                TriggerDyingSequence();
            return;
        }

        Vector3 pos = transform.position;
        pos.y = Mathf.MoveTowards(pos.y, targetY, fallSpeed * Time.deltaTime);
        transform.position = pos;

        if (Mathf.Abs(pos.y - targetY) < 0.01f)
        {
            reachedTarget = true;
            pos.y = targetY;
            transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("coin and raindrop collision");
        if (reachedTarget && (coll.CompareTag("Raindrop") || coll.CompareTag("Lightning")))
        {
            TriggerDyingSequence();
        }
    }

    private void TriggerDyingSequence()
    {
        coinAnim.SetTrigger("Die");
        StartCoroutine(DestroyAfterSeconds(dyingAnimationDuration));
    }

    private IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
