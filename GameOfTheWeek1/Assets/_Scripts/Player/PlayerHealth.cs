using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }

    [SerializeField] int health = 3;
    [SerializeField] TMP_Text healthText;
    [SerializeField] int score = 3;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] bool isInvincible = false;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        scoreText.text = "Score: " + score.ToString();
        healthText.text = "Health: " + health.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Asteroid"))
        {
            if (!isInvincible)
            {
                TakeDamage();
                Destroy(collision.gameObject);
            }
        }
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
    void TakeDamage()
    {
        health--;
        if(health <= 0)
        {
            SceneManager.LoadSceneAsync(2);
        }
        healthText.text = "Health: " + health.ToString();
        StartCoroutine(Invincible());
    }

    IEnumerator Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(2f);
        isInvincible = false;
    }
}
