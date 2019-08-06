using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunmanPink : MonoBehaviour
{
    BulletController bullet;
    GameController gameController;
    Vector3 middleOfScreen;
    bool canShoot = true;
    List<BulletController> bullets = new List<BulletController>();
    
    void Start()
    {
        bullet = GetComponentInChildren<BulletController>();
        bullet.Hide();
        middleOfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
        gameController = FindObjectOfType<GameController>();
    }
    
    void Update()
    {
        Vector3 cameraVector = Input.mousePosition - middleOfScreen;
        Vector3 flipped = new Vector3(cameraVector.x, 0f, cameraVector.y);
        transform.LookAt(flipped);

        if (Input.GetMouseButtonDown(0) && canShoot)
            StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        BulletController bulletInstance = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation);
        bulletInstance.Appear();
        bullets.Add(bulletInstance);
        yield return new WaitForSeconds(1);
        canShoot = true;
    }
}
