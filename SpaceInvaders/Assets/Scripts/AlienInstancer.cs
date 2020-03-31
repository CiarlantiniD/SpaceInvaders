using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienInstancer : MonoBehaviour
{
    [SerializeField] private int columns = 10;
    [SerializeField] private int rows = 4;
    [SerializeField] private float pandding = 0.25f;
    [Space(10)]
    [SerializeField] private GameObject alien = null;

    private float width;
    private float height;

    private float totalWidth;
    private float totalHeight;

    AlienController[,] alienControllers;

    void Start()
    {
        alienControllers = new AlienController[columns, rows];

        AlienController alienController = alien.GetComponent<AlienController>();

        width = alienController.Width;
        height = alienController.Height;

        totalWidth = width * columns + pandding * (columns - 1);
        totalHeight = height * rows + pandding * (rows - 1);

        CreateAlienInstances();
        StartCoroutine(ShootBullet());
    }

    private void CreateAlienInstances()
    {
        Vector3 position = transform.position;

        position.x -= totalWidth / 2;
        position.y -= totalHeight / 2;

        float resetX = position.x;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject alienTranform =  Instantiate(alien, position, Quaternion.identity, transform);
                alienControllers[j, i] = alienTranform.GetComponent<AlienController>();
                alienControllers[j, i].SetPositionInMatrix(j,i);
                alienControllers[j, i].OnDestroy += CheckToDestroyOthers;

                if (j != columns)
                    position.x += width + pandding;
            }

            position.x = resetX;
            position.y += height + pandding;
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

            if (valueX >= 0 && valueX < columns && 
                    valueY >= 0 && valueY < rows && 
                        currentAlienControllers.TypeID == alienControllers[valueX, valueY].TypeID && 
                            alienControllers[valueX, valueY].IsDead == false
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
            yield return new WaitForSeconds(Random.Range(1,3));

            // + if Pause

            for (int i = 0; i < rows * columns; i++)
            {
                if (alienControllers[Random.Range(0, 3), Random.Range(0, 3)].IsDead == false)
                {
                    alienControllers[Random.Range(0, 3), Random.Range(0, 3)].Shoot();
                    break;
                }
            }
            
        }
    }

}
