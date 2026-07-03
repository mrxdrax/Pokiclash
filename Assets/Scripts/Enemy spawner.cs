using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemyspawner : MonoBehaviour
{
    [System.Serializable]
    public class wave
    {
        public GameObject enemyprefab;
        public float spawnTimer;
        public float spawninterval;
        public int enemiesperwaves;
        public int spawnenemycount;
        

    }

    public Transform maxpos;
    public Transform minpos;
    public List<wave> waves;
    public int waveNumber;

    void Update()
    {
        if (playercontroller.instance.gameObject.activeSelf)
        {
            waves[waveNumber].spawnTimer += Time.deltaTime;
            if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawninterval)
            {
                waves[waveNumber].spawnTimer = 0;
                spawnenemy();

            }
            if (waves[waveNumber].spawnenemycount >= waves[waveNumber].enemiesperwaves)
            {
                waves[waveNumber].spawnenemycount = 0;
                if (waves[waveNumber].spawninterval > 0.3f)
                {
                    waves[waveNumber].spawninterval *= 0.9f;
                }
                waveNumber++;
            }
            if (waveNumber >= waves.Count)
            {
                waveNumber = 0;
            }
        }
    }
    private void spawnenemy()
    {
        Instantiate(waves[waveNumber].enemyprefab,RandomSpawnPoint(),transform.rotation);
        waves[waveNumber].spawnenemycount++;
    }
    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;
        if(Random.Range(0f,1f) > 0.5)
        {
            spawnPoint.x = Random.Range(minpos.position.x,maxpos.position.x);
            if (Random.Range(0f, 1f) > 0.5)
            {
                spawnPoint.y = minpos.position.y;
            }
            else
            {
                spawnPoint.y = maxpos.position.y;
            }
        }
        else {
            spawnPoint.y = Random.Range(minpos.position.y, maxpos.position.y);
            if (Random.Range(0f, 1f) > 0.5)
            {
                spawnPoint.x = minpos.position.x;
            }
            else
            {
                spawnPoint.x = maxpos.position.x;
            }

        }

        return spawnPoint;
    }
}
