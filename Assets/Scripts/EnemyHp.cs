using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private int hp = 3;
    private Score score;
    [SerializeField] private int val = 100;

    private void Start()
    {
        score = GameObject.FindGameObjectWithTag("Tempo").GetComponent<Score>();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            score.score += val;
            Destroy(gameObject);
        }
    }
}
