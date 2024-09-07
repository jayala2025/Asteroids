using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
    // Start is called before the first frame update
    [Header("Player parameters")]
    [SerializeField] private float shipAcceleration = 10f;
    [SerializeField] private float shipMaxVelocity = 10f;
    [SerializeField] private float shipRotationSpeed = 180f;
    [SerializeField] private float bulletSpeed = 8f;

    [Header("Object parameters")]
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Rigidbody2D bulletPrefab;


    private Rigidbody2D shipRigidBody;

    private bool isAlive = true;
    private bool isAccelerating = false;

    void Start(){
        shipRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        if(isAlive){
            HandleShipAcceleration();
            HandleShipRotation();
            HandleShooting();
        }
    }

    private void FixedUpdate() {
        if(isAccelerating && isAlive){
            shipRigidBody.AddForce(shipAcceleration * transform.up);
            shipRigidBody.velocity = Vector2.ClampMagnitude(shipRigidBody.velocity, shipMaxVelocity);
        }
    }

    private void HandleShipAcceleration() {
        isAccelerating = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    }

    private void HandleShipRotation(){
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(shipRotationSpeed * Time.deltaTime * transform.forward);
            Debug.Log("A pressed");
        } else if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-shipRotationSpeed * Time.deltaTime * transform.forward);
            Debug.Log("D pressed");
        }
    }

    private void HandleShooting(){
        if(Input.GetKeyDown(KeyCode.Space)){
            Rigidbody2D bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

            Vector2 shipVelocity = shipRigidBody.velocity;
            Vector2 shipDirection = transform.up;
            float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);

            if(shipForwardSpeed > 0){
                shipForwardSpeed = 0;
            }

            bullet.velocity = shipForwardSpeed * shipDirection;
            bullet.AddForce(bulletSpeed * transform.up, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Asteroid")){
            isAlive = false;

            GameManager gameManager = FindAnyObjectByType<GameManager>();
            gameManager.GameEnds();

            Destroy(gameObject);
            
        }
    }
}
