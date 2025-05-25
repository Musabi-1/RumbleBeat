using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAttac : MonoBehaviour
{
    private InputManager inputManager;
    private EnemyHp enemyHp;
    [SerializeField] private float attackRange = 1.5f;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        FindClosestEnemy();
        if (inputManager.attackTriggered == true)
        {
            enemyHp.TakeDamage(inputManager.charge);
            inputManager.charge = 0;
        }
    }
    
    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float sqrDistance = (enemy.transform.position - currentPosition).sqrMagnitude;
            if (sqrDistance < closestDistanceSqr && sqrDistance < attackRange)
            {
                closestDistanceSqr = sqrDistance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            enemyHp = closestEnemy.GetComponent<EnemyHp>();
        }
        else
        {
            enemyHp = null;
        }
    }
}
