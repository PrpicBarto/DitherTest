using TMPro;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] int hitsToTake;
    [SerializeField] bool largeAsteroid;
    [SerializeField] bool averageAsteroid;
    [SerializeField] bool canSpawnChildren;
    [SerializeField] GameObject averageAsteroidPrefab;
    [SerializeField] GameObject smallAsteroidPrefab;
    [SerializeField] float speed = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            CameraShake.Instance.ShakeCamera(2.5f, 0.25f);
        }
    }
    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        Destroy(gameObject, 7.5f);
    }

    public void Hit()
    {
        hitsToTake--;
        if (hitsToTake <= 0)
        {
            if (largeAsteroid && canSpawnChildren)
            {
                SpawnChildren();
                PlayerHealth.Instance.UpdateScore();
                Destroy(gameObject);
            }
            else if (averageAsteroid && canSpawnChildren)
            {
                SpawnChildren();
                PlayerHealth.Instance.UpdateScore();
                Destroy(gameObject);
            }
            else if (!canSpawnChildren)
            {
                PlayerHealth.Instance.UpdateScore();
                Destroy(gameObject);
            }
        }
    }
    void SpawnChildren()
    {
        if (largeAsteroid)
        {
            GameObject averageOne = Instantiate(averageAsteroidPrefab, transform.position, Quaternion.identity);
            Debug.Log(averageOne.name);
            GameObject averageTwo = Instantiate(averageAsteroidPrefab, transform.position, Quaternion.identity);
            Debug.Log(averageTwo.name);

            Rigidbody averageOneRigidboy = averageOne.GetComponent<Rigidbody>();
            Rigidbody averageTwoRigidboy = averageTwo.GetComponent<Rigidbody>();

            averageOneRigidboy.AddForce(new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)) * speed, ForceMode.Impulse);
            averageTwoRigidboy.AddForce(new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)) * speed, ForceMode.Impulse);

            Destroy(gameObject);
        }
        else if (averageAsteroid)
        {
            GameObject smallOne = Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);
            GameObject smallTwo = Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);

            Rigidbody smallOneRigidboy = smallOne.GetComponent<Rigidbody>();
            Rigidbody smallTwoRigidboy = smallTwo.GetComponent<Rigidbody>();

            smallOneRigidboy.AddForce(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)) * speed, ForceMode.Impulse);
            smallTwoRigidboy.AddForce(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)) * speed, ForceMode.Impulse);

            Destroy(gameObject);
        }
    }
}
