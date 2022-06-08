using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage = 10;
    public float speed = 50f;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;    
    }

    void OnTriggerEnter(Collider other)
    {
        Brick brick = other.GetComponent<Brick>();
        if (brick != null)
        {
            brick.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
