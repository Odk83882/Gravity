using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform bottomBulletSpawnPoint;
    public Transform topBulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20;

    // Add flags to control shooting for each row
    private bool bottomCanShoot = true;
    private bool topCanShoot = false;

    // Public variables for delays
    public float bottomDelay = 2f;
    public float topDelay = 2f;

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (bottomCanShoot)
            {
                // Shoot for three seconds
                for (int i = 0; i < 4; i++)
                {
                    BottomShoot();
                    yield return new WaitForSeconds(0.1f); // Adjust this for the rate of fire
                }
                bottomCanShoot = false; // Prevent shooting until delay is over
                yield return new WaitForSeconds(bottomDelay);
                bottomCanShoot = true; // Allow shooting again after delay

                // Wait for the specified delay before allowing the upper row to shoot
                yield return new WaitForSeconds(topDelay - bottomDelay);
                topCanShoot = true; // Allow shooting for the top row
            }

            if (topCanShoot)
            {
                // Shoot for three seconds
                for (int i = 0; i < 4; i++)
                {
                    TopShoot();
                    yield return new WaitForSeconds(0.1f); // Adjust this for the rate of fire
                }
                topCanShoot = false; // Prevent shooting until delay is over
                yield return new WaitForSeconds(topDelay);
                bottomCanShoot = true; // Allow shooting again for the bottom row
            }
            yield return null;
        }
    }

    void BottomShoot()
    {
        var bullet = Instantiate(bulletPrefab, bottomBulletSpawnPoint.position, bottomBulletSpawnPoint.rotation);
        // Calculate the left direction vector
        Vector2 leftDirection = -bottomBulletSpawnPoint.right; // Assuming bottomBulletSpawnPoint is facing upwards in the scene
        bullet.GetComponent<Rigidbody2D>().velocity = leftDirection * bulletSpeed;
    }

    void TopShoot()
    {
        var bullet = Instantiate(bulletPrefab, topBulletSpawnPoint.position, topBulletSpawnPoint.rotation);
        // Calculate the left direction vector
        Vector2 leftDirection = -topBulletSpawnPoint.right; // Assuming topBulletSpawnPoint is facing upwards in the scene
        bullet.GetComponent<Rigidbody2D>().velocity = leftDirection * bulletSpeed;
    }
}
