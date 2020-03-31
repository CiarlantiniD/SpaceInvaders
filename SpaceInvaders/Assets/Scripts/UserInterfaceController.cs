using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{
    [SerializeField] private UILifeIndicator lifesIndicator;
    

    public void LifesToShow(int values)
    {
        lifesIndicator.ShowValue(values);
    }

}
