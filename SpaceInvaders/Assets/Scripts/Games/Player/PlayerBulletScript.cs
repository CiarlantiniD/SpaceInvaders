using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
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
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPaused)
            return;

        transform.Translate(Vector3.up * 0.1f);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Alien" || collider.gameObject.tag == "Shield")
            gameObject.SetActive(false);
    }

}
