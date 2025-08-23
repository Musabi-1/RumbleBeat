using UnityEngine;

public class RainDropMove : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float margin = 1f;

    private float currentSpeed = 0f;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime, Space.World);

        if (!IsVisibleFrom(mainCam, margin))
        {
            Destroy(gameObject);
        }
    }

    private bool IsVisibleFrom(Camera cam, float margin)
    {
        if (cam == null) return false;

        Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);

        if (viewportPos.z < 0) return false;

        if (viewportPos.x < -margin || viewportPos.x > 1 + margin) return false;
        if (viewportPos.y < -margin || viewportPos.y > 1 + margin) return false;

        return true;
    }
}
