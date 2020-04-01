using System.Collections;
using UnityEngine;

public class AlienBulletController : MonoBehaviour
{
    private AlienController[,] alienControllers;
    private AlienInstancerConfiguration configuration;

    private bool isPause;

    private IEnumerator ShootBulletCoroutine;
 


    public void SetAlienControllers(AlienController[,] alienControllers) 
    {
        this.alienControllers = alienControllers;
    }

    public void SetAlienInstancerConfiguration(AlienInstancerConfiguration configuration)
    {
        this.configuration = configuration;
    }



    public void StartBullets()
    {
        ShootBulletCoroutine = ShootBullet();
        StartCoroutine(ShootBulletCoroutine);
    }

    public void StopBullets()
    {
        StopCoroutine(ShootBulletCoroutine);
    }


    public void PauseBullets()
    {
        isPause = true;
    }

    public void UnpauseBullets()
    {
        isPause = false;
    }

    



    private IEnumerator ShootBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 3));

            if (isPause)
                continue;

            for (int i = 0; i < configuration.total; i++)
            {
                AlienController current = alienControllers[Random.Range(0, configuration.columns), Random.Range(0, configuration.rows)];

                if (current.IsAlive)
                {
                    current.Shoot();
                    break;
                }
            }
        }
    }


}
