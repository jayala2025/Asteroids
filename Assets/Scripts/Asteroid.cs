using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float size = 4;

    public GameManager gameManager;
    // Start is called before the first frame update
    private void Start()
    {
        transform.localScale = 0.1f * size * Vector3.one;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float spawnSpeed = Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);

        gameManager.asteroidCount++;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Bullet")){
            gameManager.asteroidCount--;
            Destroy(collision.gameObject);

            if(size > 1){
                for(int i = 0; i < 2; i++){
                    Asteroid smallerAsteroid = Instantiate(this, transform.position, Quaternion.identity);
                    smallerAsteroid.size = size - 1;
                    smallerAsteroid.gameManager = gameManager;
                }
            }
        }

        Destroy(gameObject);

    }

}
