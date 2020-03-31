using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UserInterfaceController UIController;
    [SerializeField] private GameObject player;


    static public Action OnPlayerDestroy;

    public int PlayerLifes { get; private set; } = 3;

    private void Start()
    {
        UIController.LifesToShow(PlayerLifes);

        OnPlayerDestroy += PlayerDestroy;
    }



    private void CreatePlayer()
    {
        Instantiate(player);
    }

    private void PlayerDestroy()
    {
        StartCoroutine(PlayerRecoveryAnimation());
    }

    IEnumerator PlayerRecoveryAnimation()
    {
        yield return new WaitForSeconds(2);
        CreatePlayer();
    }



}
