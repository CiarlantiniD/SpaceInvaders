using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform bulletPlayer;

    private bool isAlive;
    private bool isPaused;

    private const float MIN_X = -6;
    private const float MAX_X = 6;

    void Start()
    {
        isAlive = true;

        GameManager.OnPause += PausePlayer;
        GameManager.OnUnpause += UnpausePlayer;
    }

    private void OnDestroy()
    {
        GameManager.OnPause -= PausePlayer;
        GameManager.OnUnpause -= UnpausePlayer;
    }


    void Update()
    {
        if (isPaused)
            return;
        
        Shoot();

        Move();
        CheckRangeMovement();
    }


    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlaySound(AudioManager.Sounds.ShootPlayer);
            Instantiate(bulletPlayer, transform.localPosition, Quaternion.identity);
        }
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


    private void CheckRangeMovement()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, MIN_X, MAX_X);
        transform.position = position;
    }


    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Alien" || collision.gameObject.tag == "AlienBullet")
            Destroy();
    }

    public void Destroy()
    {
        if (isAlive == false)
            return;

        AudioManager.Instance.PlaySound(AudioManager.Sounds.ExplotionPlayer);
        GameManager.OnPlayerDestroy?.Invoke();
        Destroy(gameObject);
    }


    private void PausePlayer()
    {
        isPaused = true;
    }

    private void UnpausePlayer()
    {
        isPaused = false;
    }

}
