using System.Collections;
using UnityEngine;

public class KumoMove : MonoBehaviour
{
    [SerializeField] private float rightLimit = 15;
    [SerializeField] private float leftLimit = -15;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float stopTime = 1;

    private bool movingRight = false, isWaiting = false;

    private void Update()
    {
        if (!isWaiting)
        {
            float direction = movingRight ? 1f : -1f;
            transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

            if (!movingRight && transform.position.x <= leftLimit)
            {
                StartCoroutine(SwitchDirection());
            }
            else if (movingRight && transform.position.x >= rightLimit)
            {
                StartCoroutine(SwitchDirection());
            }
        }
    }

    private IEnumerator SwitchDirection()
    {
        isWaiting = true;
        yield return new WaitForSeconds(stopTime);
        movingRight = !movingRight;
        isWaiting = false;
    }
}
