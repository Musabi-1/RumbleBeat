using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempoManager : MonoBehaviour
{
    [SerializeField] public float bpm = 2f;
    public float enemybpm = 3f;
    public bool enemybeat = false;
    public bool tap = false;
    private float timeCount = 0f;
    [SerializeField] private float scaleBuffer = 0.1f;
    [SerializeField] private float TrackTime = 180f;
    private float leftTime;
    [SerializeField] private Image TimeBar;

    private void Update()
    {
        timeCount += Time.deltaTime;
        leftTime = TrackTime - timeCount;
        TimeBar.fillAmount = leftTime / TrackTime;
        if (timeCount % bpm < scaleBuffer || timeCount % bpm > bpm - scaleBuffer)
        {
            tap = true;
        }
        else
        {
            tap = false;
        }

        if (timeCount % enemybpm < scaleBuffer || timeCount % enemybpm > enemybpm - scaleBuffer)
        {
            enemybeat = true;
            Debug.Log("enemy");
        }
        else
        {
            enemybeat = false;
        }
    }
}
