using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakanaBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [HideInInspector] public bool above = false;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        HorizontalMovement();

        Vector3 viewPortPos = mainCam.WorldToViewportPoint(transform.position);
        if (viewPortPos.x < 0f)
            Destroy(gameObject);
    }

    private void HorizontalMovement()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void VerticalMovement()
    {
        if (above)
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
