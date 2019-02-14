using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanSpawner : MonoBehaviour
{

    public GameObject human;
    public float spawnRate;
    private float spawnTimer;
    private List<GameObject> humans = new List<GameObject>();
    public int maxHumans;
    public static int numberOfHumans;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0;
        GameObject homeboy = Instantiate(human, transform.position, Quaternion.identity);
        homeboy.transform.localEulerAngles = new Vector3(-90, 0, 0);
        humans.Add(homeboy);
        print(humans.Count);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate && humans.Count < maxHumans)
        {
           GameObject homeboy = Instantiate(human, transform.position, Quaternion.identity);
            homeboy.transform.localEulerAngles = new Vector3(-90, 0, 0);
            humans.Add(homeboy);
            print(humans.Count);
            spawnTimer = 0;
        }
        numberOfHumans = humans.Count;
    }
}
