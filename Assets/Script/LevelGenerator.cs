using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // public GameObject platformPrefab;
    public GameObject greenPlatformPrefab;  // Normale
    public GameObject bluePlatformPrefab;   // Boost
    public GameObject redPlatformPrefab;    // Mobile
    public int maxPlatforms = 10; // Nombre max de plateformes visibles
    public float levelWidth = 1.18f;
    public float minY = 0.2f;
    public float maxY = 1.5f;
    public Transform player; // Référence au joueur

    private List<GameObject> platforms = new List<GameObject>();
    private float highestY; // Position la plus haute d'une plateforme

    void Start()
    {
        // Génération initiale des plateformes
        Vector3 spawnPosition = new Vector3(0, -2f, 0); // Position initiale basse

        for (int i = 0; i < maxPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth / 2, levelWidth / 2);

            GameObject platform = Instantiate(GetRandomPlatform(), spawnPosition, Quaternion.identity);
            platforms.Add(platform);

            highestY = spawnPosition.y;
        }
    }

    void Update()
    {
        // Générer une nouvelle plateforme si le joueur monte
        if (player.position.y + 5f > highestY) // Si le joueur approche du sommet
        {
            SpawnNewPlatform();
        }

        // Supprimer les plateformes trop basses
        RemoveOldPlatforms();
    }

    void SpawnNewPlatform()
    {
        Vector3 spawnPosition = new Vector3();
        spawnPosition.y = highestY + Random.Range(minY, maxY);
        spawnPosition.x = Random.Range(-levelWidth / 2, levelWidth / 2);

        GameObject platform = Instantiate(GetRandomPlatform(), spawnPosition, Quaternion.identity);
        platforms.Add(platform);

        highestY = spawnPosition.y;
    }

    void RemoveOldPlatforms()
    {
        if (platforms.Count > maxPlatforms)
        {
            GameObject oldPlatform = platforms[0];
            if (oldPlatform.transform.position.y < player.position.y - 5f) // Si hors écran
            {
                platforms.RemoveAt(0);
                Destroy(oldPlatform);
            }
        }
    }

    GameObject GetRandomPlatform()
    {
        int random = Random.Range(0, 10); // Probabilités : 60% verte, 20% bleue, 20% rouge

        if (random < 6) return greenPlatformPrefab;   // 60% chance
        if (random < 8) return bluePlatformPrefab;    // 20% chance
        return redPlatformPrefab;                     // 20% chance
    }
    
}
