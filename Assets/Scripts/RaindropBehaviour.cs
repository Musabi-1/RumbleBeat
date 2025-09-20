using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaindropBehaviour : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 4f;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        Vector3 viewportPos = mainCam.WorldToViewportPoint(transform.position);
        if (viewportPos.y < 0f)
        {
            Destroy(gameObject);
        }
    }
}
