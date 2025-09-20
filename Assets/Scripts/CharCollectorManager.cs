using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CharCollectorManager : MonoBehaviour
{
    [SerializeField] private float stunDuration = 1.5f;
    [SerializeField] private TextMeshProUGUI coinText;
    private int coinCount = 0;
    private CharacterMovement movement;
    private bool isStunned = false;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
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
            coinCount++;
            DestroyWholeObject(other);
            UpdateCoinUI();
        }
        else if (other.CompareTag("Raindrop"))
        {
            if (!isStunned)
            {
                DestroyWholeObject(other);

                if (movement != null)
                    movement.Stun(stunDuration);

                StartCoroutine(StunCooldown());
            }
        }
    }

    private IEnumerator StunCooldown()
    {
        isStunned = true;
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
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
