using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoController : MonoBehaviour
{
    Rigidbody body;
    Vector3 movement;
    bool isDead = false;

    public float speed = 1f;
    public float deathRotationSpeed = 100f;

    void Start()
    {
        movement = new Vector3(speed, 0, 0);
        body = GetComponent<Rigidbody>();
        //transform.Rotate(new Vector3(0, 180, 0), Space.Self);

        // spawns: 
        // x: entre -6 e 6
        // z: entre 1 e -1.5
    }

    void FixedUpdate()
    {
        if (isDead)
            Spin();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Bullet")
            return;
        
        body.useGravity = true;
        body.AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);
        isDead = true;
        Destroy(collider.gameObject);
    }

    void Move()
    {
        body.AddForce(movement, ForceMode.Force);
    }

    void Spin()
    {
        transform.Rotate(new Vector3(0, 0, deathRotationSpeed) * Time.deltaTime, Space.Self);
    }
}
