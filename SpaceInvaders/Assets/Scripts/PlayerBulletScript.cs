using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{

    private void Awake()
    {
        Destroy(gameObject, 1);
    }

    void Update()
    {
        transform.Translate(Vector3.up * 0.1f);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Alien")
            Destroy(gameObject);
    }

}
