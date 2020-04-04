using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProtectionScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "AlienBullet" || collider.gameObject.tag == "PlayerBullet")
            Destroy(gameObject);
    }
}
