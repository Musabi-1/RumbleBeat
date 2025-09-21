using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaindropBehaviour : MonoBehaviour
{
    [SerializeField] private float defaultFallAcceleration = 3f;
    [SerializeField] private float randomBias = 1f;
    [SerializeField] private float initialVelocity = 0f;
    private Camera mainCam;
    private float fallAcceleration;
    private float velocity;

    private void Awake()
    {
        mainCam = Camera.main;
        AccelRandomizor();
    }

    private void Update()
    {
        velocity += fallAcceleration * Time.deltaTime;
        transform.position += Vector3.down * velocity * Time.deltaTime;

        Vector3 viewportPos = mainCam.WorldToViewportPoint(transform.position);
        if (viewportPos.y < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void AccelRandomizor()
    {
        fallAcceleration = defaultFallAcceleration * randomBias * Random.Range(0.5f, 1.5f);
        velocity = initialVelocity * Random.Range(0.5f, 1.5f);
    }
}
