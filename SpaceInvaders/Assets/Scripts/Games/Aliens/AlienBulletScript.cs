using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBulletScript : MonoBehaviour
{
    private bool isPaused;
    
    private void Start()
    {
        GameManager.OnPause += PauseBullet;
        GameManager.OnUnpause += UnpauseBullet;
    }

    void PauseBullet()
    {
        isPaused = true;
    }

    void UnpauseBullet()
    {
        isPaused = false;
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (isPaused)
            return;

        transform.Translate(Vector3.down * 0.08f);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Shield")
            Destroy(gameObject);
    }
}
