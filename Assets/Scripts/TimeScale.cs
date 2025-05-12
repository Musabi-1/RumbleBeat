using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    [SerializeField] GameObject scale;
    private float incriment;
    private void Start()
    {
        TempoManager tempoManager = GameObject.FindGameObjectWithTag("Tempo").GetComponent<TempoManager>();
        incriment = tempoManager.bpm;
        InitializeScales();
        StartCoroutine(ScaleSpawner());
    }

    private IEnumerator ScaleSpawner(){
        while(true){
        GameObject scales = Instantiate(scale, transform);
        RectTransform rt = scales.GetComponent<RectTransform>();

        rt.anchoredPosition = new Vector2((transform.root.GetComponent<RectTransform>().rect.width/8) * 7,0f);

        yield return new WaitForSeconds(incriment);
        }
    }

    private void InitializeScales(){
        for(int i = 1; i<7; i++){
            GameObject obj = Instantiate(scale, transform);
            RectTransform rt = obj.GetComponent<RectTransform>();

            rt.anchoredPosition = new Vector2((transform.root.GetComponent<RectTransform>().rect.width/8) * i, 0f);
        }
    }
}
