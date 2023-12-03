using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Asteroid asteroid;

    float spawnRate = 2.0f;
    float spawnDistance = 14f;

    void Start()
    {
        InvokeRepeating("spawn", 0f, spawnRate);
    }

    void spawn()
    {
        Vector2 spawnPoint = Random.insideUnitCircle.normalized * spawnDistance;
        float angle = Random.Range(-15f, 15f);
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0,0,1));
        Asteroid theAsteroid = Instantiate(asteroid, spawnPoint, rotation);
        Vector2 direction = rotation * -spawnPoint;
        float mass = Random.Range(0.8f, 1.4f);

        theAsteroid.kick(mass, direction);
    }
}
