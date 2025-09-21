using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDestroy : MonoBehaviour
{
    public void DestroyObj()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
