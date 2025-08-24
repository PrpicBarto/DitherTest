using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] float spawnInterval = 2.0f;
    [SerializeField] float spawnRadius = 10.0f;
    [SerializeField] int waveCount = 0;
    [SerializeField] int currentWave = 10;
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] asteroids;
    [SerializeField] TMP_Text waveText;
    private void Awake()
    {
        waveCount = 0;
        currentWave = 10;
                waveText.text = "Waves left: " + currentWave.ToString();
    }
    private void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 0f, spawnInterval);
    }
    private void SpawnAsteroid()
    {
        if(currentWave <= 10)
        {
            waveCount++;
            if (waveCount <= 10)
            {
                Vector3 spawnPosition = Random.onUnitSphere * spawnRadius;
                spawnPosition.y = Mathf.Abs(spawnPosition.y);
                GameObject asteroid = Instantiate(asteroids[Random.Range(0, 3)], spawnPosition, Quaternion.identity);
                Asteroid asteroidComponent = asteroid.GetComponent<Asteroid>();

                asteroidComponent.SetTarget(player.transform.position);
            }
            else if(waveCount > 10)
            {
                currentWave--;
                waveCount = 0;
                waveText.text = "Waves left: " + currentWave.ToString();
                spawnInterval -= 0.15f;
            }
        }
        else if(currentWave <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}
