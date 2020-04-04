using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILifeIndicator : MonoBehaviour
{
    [SerializeField] private GameObject life = null;


    public void ShowValue(int value)
    {
        int lifes = transform.childCount;

        if (value < lifes)
        {
            for (int i = 0; i < lifes - value; i++)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
        else if (value > lifes)
        {
            for (int i = 0; i < value - lifes; i++)
            {
                Instantiate(life, transform);
            }
        }
    }
}
