using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Asteroid asteroid))
        {
            asteroid.Hit();
            Destroy(gameObject);
        }
    }
}
