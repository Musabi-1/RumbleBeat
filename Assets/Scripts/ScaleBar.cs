using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBar : MonoBehaviour
{
    private float moveSpeed;
    private RectTransform rectTransform;
    private float incriment;

    private void Start(){
        TempoManager tempoManager = GameObject.FindGameObjectWithTag("Tempo").GetComponent<TempoManager>();
        rectTransform = GetComponent<RectTransform>();
        incriment = transform.root.GetComponent<RectTransform>().rect.width / 8;
        moveSpeed = incriment / tempoManager.bpm;
    }

    private void Update()
    {
        rectTransform.anchoredPosition -= new Vector2(moveSpeed * Time.deltaTime, 0f);

        if(rectTransform.localPosition.x < incriment){
            Destroy(gameObject);
        }
    }
}
