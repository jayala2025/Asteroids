using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifetime = 1f;
    // Start is called before the first frame update
    private void Awake() {
        Destroy(gameObject, bulletLifetime);    
    }
}
