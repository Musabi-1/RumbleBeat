using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    [SerializeField] private float interval = 15f;
    [SerializeField] private float lightningSpan = 2f;
    [SerializeField] private float prepTime = 0.5f;
    [SerializeField] private GameObject lightningSprite;

    private float countDown = 0f;
    private GameObject lightning;
    [HideInInspector] public bool isOn = false;
    private Animator cloudAnim;

    private void Start()
    {
        cloudAnim = gameObject.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        countDown += Time.deltaTime;
        if (countDown >= interval && !isOn)
        {
            StartCoroutine(StartLightning());
            countDown = 0;
        }
    }

    private IEnumerator StartLightning() {
        cloudAnim.SetBool("Strike", true);
        yield return new WaitForSeconds(prepTime);

        isOn = true;
        Vector3 spawnPos = new Vector3(transform.position.x, 0f, 0f);
        lightning = Instantiate(lightningSprite, spawnPos, Quaternion.identity);

        yield return new WaitForSeconds(lightningSpan);

        cloudAnim.SetBool("Strike", false);
        Destroy(lightning);
        lightning = null;
        isOn = false;
    }

}
