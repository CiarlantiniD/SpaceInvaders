using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuUserInterfaceController : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject leyenda;

    [Space(10)]
    [SerializeField] private Animator startTextAnimator;



    public void TurnOff() 
    {
        logo.SetActive(false);
        startText.SetActive(false);
        leyenda.SetActive(false);
    }

    public void TurnOn()
    {
        logo.SetActive(true);
        startText.SetActive(true);
        leyenda.SetActive(true);
    }


    public void TriggerStartTextAnimation()
    {
        startTextAnimator.SetTrigger("Start");
    }

}
