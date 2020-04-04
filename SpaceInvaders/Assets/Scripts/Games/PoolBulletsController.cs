using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBulletsController : MonoBehaviour
{
    [SerializeField] private GameObject PlayerBullet;
    [SerializeField] private int amountPlayerBullet = 1;
    
    [Space]
    [SerializeField] private GameObject AlienBullet;
    [SerializeField] private int amountAlienBullet = 5;


    private List<GameObject> pooledBulletPlayer;
    private List<GameObject> pooledBulletAlien;


    private void Start()
    {
        pooledBulletPlayer = AddInstancesOfObject(PlayerBullet, amountPlayerBullet);
        pooledBulletAlien = AddInstancesOfObject(AlienBullet, amountAlienBullet);
    }

    private List<GameObject> AddInstancesOfObject(GameObject gameObject, int amount)
    {
        List<GameObject> pooledObject = new List<GameObject>();

        for (int i = 0; i < amount; i++)
        {
            GameObject instance = (GameObject)Instantiate(gameObject, transform);
            instance.SetActive(false);
            pooledObject.Add(instance);
        }

        return pooledObject;
    }


    public void ShootPlayerBullet(Vector3 position)
    {
        for (int i = 0; i < pooledBulletPlayer.Count; i++)
        {
            if (pooledBulletPlayer[i].activeInHierarchy == false)
            {
                AudioManager.Instance.PlaySound(AudioManager.Sounds.ShootPlayer);
                pooledBulletPlayer[i].transform.position = position;
                pooledBulletPlayer[i].SetActive(true);
            }
                 
        }
    }

    public void ShootAlienBullet(Vector3 position)
    {
        for (int i = 0; i < pooledBulletAlien.Count; i++)
        {
            if (pooledBulletAlien[i].activeInHierarchy == false)
            {
                pooledBulletAlien[i].transform.position = position;
                pooledBulletAlien[i].SetActive(true);
            }

        }
    }

}
