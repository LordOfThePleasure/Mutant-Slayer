using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private string enemyTag;
    [SerializeField] private float topBound, downBound, leftBound, rightBound;
    [SerializeField] private int amountOfEnemies;

    private void SpawnEnemy()
    {
        float x = Random.Range(leftBound, rightBound);
        float y = Random.Range(downBound, topBound);

        ObjectPooler.instance.Spawn(enemyTag, new Vector2(x, y), Quaternion.identity);
    }

    public void DefaultSpawn()
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            SpawnEnemy();
        }
    }
}
