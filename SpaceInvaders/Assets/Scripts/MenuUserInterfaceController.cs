using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuUserInterfaceController : MonoBehaviour
{
    [SerializeField] private GameObject bestScore;

    [Space(10)]
    [SerializeField] private Animator startTextAnimator;



    public void TurnOff() 
    {
        gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }


    public void TriggerStartTextAnimation()
    {
        startTextAnimator.SetTrigger("Start");
    }


    public void SetBestScore(int score)
    {
        bestScore.SetActive(true);
        bestScore.GetComponent<Text>().text = "Best Score - " + score.ToString("00000000000");
    }

}
