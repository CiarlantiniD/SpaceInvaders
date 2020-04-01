using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct AlienInstancerConfiguration
{
    public readonly int columns;
    public readonly int rows;
    public readonly float pandding;

    public AlienInstancerConfiguration(int columns, int rows, float pandding)
    {
        this.columns = columns;
        this.rows = rows;
        this.pandding = pandding;
    }
}


public class AlienInstancer : MonoBehaviour
{
    [SerializeField] private GameObject alien = null;
    [SerializeField] private AlienTranslation alienTranslation;

    private AlienInstancerConfiguration configuration;

    private float width;
    private float height;

    private float totalWidth;
    private float totalHeight;

    private int totalAliens;
    private int totalAlienDeaths;

    AlienController[,] alienControllers;

    private IEnumerator ShootBulletCoroutine;

    public void SetConfiguration(AlienInstancerConfiguration config)
    {
        configuration = config;

        totalAliens = configuration.columns * configuration.rows;

        alienControllers = new AlienController[configuration.columns, configuration.rows];

        AlienController alienController = alien.GetComponent<AlienController>();

        width = alienController.Width;
        height = alienController.Height;

        totalWidth = width * configuration.columns + configuration.pandding * (configuration.columns - 1);
        totalHeight = height * configuration.rows + configuration.pandding * (configuration.rows - 1);

    }


    public void CreateAliens()
    {
        CreateAlienInstances();
        alienTranslation.StartTranslate();

        ShootBulletCoroutine = ShootBullet();
        StartCoroutine(ShootBulletCoroutine);
    }



    private void CreateAlienInstances()
    {
        Vector3 position = transform.position;

        position.x -= totalWidth / 2;
        position.y -= totalHeight / 2;

        float resetX = position.x;

        for (int i = 0; i < configuration.rows; i++)
        {
            for (int j = 0; j < configuration.columns; j++)
            {
                GameObject alienTranform =  Instantiate(alien, position, Quaternion.identity, transform);
                alienControllers[j, i] = alienTranform.GetComponent<AlienController>();
                alienControllers[j, i].SetPositionInMatrix(j,i);
                alienControllers[j, i].OnDestroy += OnAlienDeath;

                if (j != configuration.columns)
                    position.x += width + configuration.pandding;
            }

            position.x = resetX;
            position.y += height + configuration.pandding;
        }
    }

   


    private void OnAlienDeath(Vector2 alienPositionInMatrix)
    {
        ++totalAlienDeaths;

        if (totalAlienDeaths == totalAliens)
        {
            StopCoroutine(ShootBulletCoroutine);
            alienTranslation.StopTranslate();
            GameManager.OnAllAliensDestroy?.Invoke();
        }
        else
        {
            CheckToDestroyOthers(alienPositionInMatrix);
        }
    }


    private void CheckToDestroyOthers(Vector2 alienPositionInMatrix)
    {
        Vector2 [] positionsToCheck = new Vector2[4];

        positionsToCheck[0] = new Vector2(alienPositionInMatrix.x, alienPositionInMatrix.y + 1);
        positionsToCheck[1] = new Vector2(alienPositionInMatrix.x, alienPositionInMatrix.y - 1);
        positionsToCheck[2] = new Vector2(alienPositionInMatrix.x - 1, alienPositionInMatrix.y);
        positionsToCheck[3] = new Vector2(alienPositionInMatrix.x + 1, alienPositionInMatrix.y);

        AlienController currentAlienControllers = alienControllers[(int)alienPositionInMatrix.x, (int)alienPositionInMatrix.y];

        for (int i = 0; i < positionsToCheck.Length; i++)
        {
            int valueX = (int)positionsToCheck[i].x;
            int valueY = (int)positionsToCheck[i].y;

            if (valueX >= 0 && valueX < configuration.columns && 
                    valueY >= 0 && valueY < configuration.rows && 
                        currentAlienControllers.TypeID == alienControllers[valueX, valueY].TypeID && 
                            alienControllers[valueX, valueY].IsAlive
            ) 
            {
                alienControllers[valueX, valueY].Destoy();
            }
        }
    }


    IEnumerator ShootBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1,3));

            // + if Pause

            for (int i = 0; i < configuration.rows * configuration.columns; i++)
            {
                if (alienControllers[UnityEngine.Random.Range(0, 3), UnityEngine.Random.Range(0, 3)].IsAlive)
                {
                    alienControllers[UnityEngine.Random.Range(0, 3), UnityEngine.Random.Range(0, 3)].Shoot();
                    break;
                }
            }
            
        }
    }


    public void ResetAlines()
    {
        foreach (AlienController item in alienControllers)
        {
            item.OnReset();
        }

        alienTranslation.StartTranslate();

        ShootBulletCoroutine = ShootBullet();
        StartCoroutine(ShootBulletCoroutine);
    }


}
