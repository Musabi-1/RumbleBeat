using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeScale : MonoBehaviour
{
    [SerializeField] private GameObject scale;
    [SerializeField] private Image Metronome;
    private Color baseColor;
    private Vector3 baseScale;
    private TempoManager tempoManager;
    private InputManager inputManager;

    private float incriment;
    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<InputManager>();
        tempoManager = GameObject.FindGameObjectWithTag("Tempo").GetComponent<TempoManager>();
        incriment = tempoManager.bpm;
        InitializeScales();
        StartCoroutine(ScaleSpawner());
        baseScale = Metronome.rectTransform.localScale;
        baseColor = Metronome.color;

        GameObject.FindGameObjectWithTag("BackGround").GetComponent<AudioSource>().enabled = true;
    }

    private void Update()
    {
        if (tempoManager.tap)
        {
            Metronome.rectTransform.localScale = baseScale * 1.4f;
        }
        else
        {
            Metronome.rectTransform.localScale = baseScale;
        }
        if (inputManager.failed)
        {
            StartCoroutine(metronomeFail());
        }
    }

    private IEnumerator ScaleSpawner()
    {
        while (true)
        {
            GameObject scales = Instantiate(scale, transform);
            RectTransform rt = scales.GetComponent<RectTransform>();

            rt.anchoredPosition = new Vector2((transform.root.GetComponent<RectTransform>().rect.width / 8) * 7, 0f);

            yield return new WaitForSeconds(incriment);
        }
    }

    private void InitializeScales()
    {
        for (int i = 1; i < 7; i++)
        {
            GameObject obj = Instantiate(scale, transform);
            RectTransform rt = obj.GetComponent<RectTransform>();

            rt.anchoredPosition = new Vector2((transform.root.GetComponent<RectTransform>().rect.width / 8) * i, 0f);
        }
    }

    private IEnumerator metronomeFail()
    {
        Metronome.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        Metronome.color = baseColor;
    }
}
