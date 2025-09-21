using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CharCollectorManager : MonoBehaviour
{
    [SerializeField] private float stunDuration = 1.5f;
    [SerializeField] private float lightningStun = 2.5f;
    [SerializeField] private TextMeshProUGUI coinText;
    private int coinCount = 0;
    private CharacterMovement movement;
    private bool isStunned = false;
    private Animator diabloAnim;
    private Animator coinAnim;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        diabloAnim = GetComponentInChildren<Animator>();
        if (movement == null)
            Debug.LogError("CharacterMovement script not found on this obj!");
    }

    private void Start()
    {
        UpdateCoinUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coinAnim = other.GetComponentInChildren<Animator>();
            coinAnim.SetTrigger("Collect");
            coinCount++;
            UpdateCoinUI();
        }
        else if (other.CompareTag("Raindrop"))
        {
            if (!isStunned)
            {
                DestroyWholeObject(other);

                if (movement != null)
                    movement.Stun(stunDuration);

                StartCoroutine(StunCooldown(stunDuration));
            }
        }
        else if (other.CompareTag("Lightning"))
        {
            if (movement != null)
                movement.Stun(lightningStun);
            StartCoroutine(StunCooldown(lightningStun));
        }
    }

    private IEnumerator StunCooldown(float secs)
    {
        isStunned = true;
        diabloAnim.SetBool("Stun", true);
        yield return new WaitForSeconds(secs);
        isStunned = false;
        diabloAnim.SetBool("Stun", false);
    }

    private void DestroyWholeObject(Collider2D other)
    {
        if (other.transform.parent != null)
            Destroy(other.transform.parent.gameObject);
        else
            Destroy(other.gameObject);
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount.ToString();
        }
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}
