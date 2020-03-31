using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform bulletPlayer;

    private bool isAlive;
    
    void Start()
    {
        isAlive = true;
    }

    void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * 0.1f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * 0.1f);
        }
    }


    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPlayer, transform.localPosition, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Alien" || collision.gameObject.tag == "AlienBullet")
            Hit();
    }

    void Hit()
    {
        if (isAlive == false)
            return;

        GameManager.OnPlayerDestroy?.Invoke();
        Destroy(gameObject);
    }

}
