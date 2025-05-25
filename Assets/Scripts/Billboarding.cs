using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 lookDirection = cameraPosition - transform.position;

        lookDirection.x = 0;

        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
