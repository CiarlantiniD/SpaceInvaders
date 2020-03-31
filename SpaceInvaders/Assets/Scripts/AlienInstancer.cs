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

    private void CheckToDestroyOthers(Vector2 vector2)
    {
        Debug.Log("Murio el Alien en la posición: x" + vector2.x + "  /  y" + vector2.y);
    }

}
