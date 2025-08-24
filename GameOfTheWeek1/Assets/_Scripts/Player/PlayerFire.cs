using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float maxTimeBetweenBullets;
    [SerializeField] float currentTimeBetweenBullets;

    private void Start()
    {
        currentTimeBetweenBullets = maxTimeBetweenBullets;
    }
    void Update()
    {
        currentTimeBetweenBullets -= Time.deltaTime;

        if(Input.GetMouseButton(0))
        {
            if(currentTimeBetweenBullets <= 0)
            {
                Fire();
                currentTimeBetweenBullets = maxTimeBetweenBullets;
            }
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(transform.forward * 50f, ForceMode.Impulse);
        Destroy(bullet, 2f);
    }
}
