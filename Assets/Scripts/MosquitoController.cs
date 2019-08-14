using UnityEngine;

public class MosquitoController : MonoBehaviour
{
    GameController gameController;
    Rigidbody body;
    Vector3 movement;
    Vector3 flipFix = new Vector3(0, 0, -2f);
    bool isDead = false;
    float speed = 20f;

    public float minimumSpeed = 10f;
    public float maximumSpeed = 30f;
    public float deathRotationSpeed = 300f;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        speed = Random.Range(minimumSpeed, maximumSpeed);
        movement = new Vector3(speed, 0, 0);
        body = GetComponent<Rigidbody>();
        Spawn();
    }

    void Spawn()
    {
        float z = Random.Range(2f, 4.5f);
        bool isLeft = System.Convert.ToBoolean(Random.Range(0, 2));

        if (isLeft)
            TurnLeft();

        transform.position = new Vector3(movement.x < 0 ? 12f : -9f, -0.932f, z) + flipFix;
    }

    void TurnLeft()
    {
        transform.Rotate(new Vector3(0, 180, 0), Space.Self);
        movement *= -1;
    }

    void FixedUpdate()
    {
        Move();

        if (isDead)
        {
            Spin();

            if (transform.position.y < -50)
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Bullet" || isDead)
            return;
        
        body.useGravity = true;
        body.AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);
        isDead = true;
        Destroy(collider.gameObject);
        
        if (transform.position.z <= .5f)
        {
            gameController.score += 10;

            if (Random.Range(0, 2) == 0)
                gameController.bulletsCount++;
        }
        else if (transform.position.z <= 1.5f)
        {
            gameController.score += 20;
            gameController.bulletsCount++;
        }
        else if (transform.position.z <= 2f)
        {
            gameController.score += 30;
            gameController.bulletsCount++;

            if (Random.Range(0, 2) == 0)
                gameController.bulletsCount++;
        }
        else
        {
            gameController.score += 40;
            gameController.bulletsCount += 2;
        }
    }

    void Move()
    {
        transform.position += movement * Time.deltaTime;

        if (transform.position.x > 12 || transform.position.x < -12)
            Destroy(gameObject);
    }

    void Spin()
    {
        if (transform.rotation.eulerAngles.z > 90)
            return;

        transform.Rotate(new Vector3(0, 0, deathRotationSpeed) * Time.deltaTime, Space.Self);
    }
}
