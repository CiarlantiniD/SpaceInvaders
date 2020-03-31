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

    void Start()
    {
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
                Instantiate(alien, position, Quaternion.identity, transform);

                if(j != columns)
                    position.x += width + pandding;
            }

            position.x = resetX;
            position.y += height + pandding;
        }
    }

}
