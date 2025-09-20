using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 6f;

    private float targetY;
    private bool reachedTarget = false;

    public void SetTargetY(float y)
    {
        targetY = y;
    }

    private void Update()
    {
        if (reachedTarget) return;

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
}
