using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanSpawner : MonoBehaviour
{

    public GameObject human;
    public float spawnRate;
    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            Instantiate(human, transform.position, Quaternion.identity);
            spawnTimer = 0;
        }
    }
}
