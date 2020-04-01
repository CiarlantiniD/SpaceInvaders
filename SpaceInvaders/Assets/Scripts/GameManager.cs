using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UserInterfaceController UIController;
    [SerializeField] private GameObject player;


    static public Action OnPlayerDestroy;
    static public Action OnAllAliensDestroy;

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
        PlayerLifes -= 1;

        UIController.LifesToShow(PlayerLifes);

        if (PlayerLifes > 0)
        {
            StartCoroutine(PlayerRecoveryAnimation());

        }
        else
        {
            Debug.Log("Game Over");
        }

    }

    IEnumerator PlayerRecoveryAnimation()
    {
        yield return new WaitForSeconds(2);
        CreatePlayer();
    }

    private void RestartLevel()
    {
        
        
        // + Animacion
        // + Reposicionar Player
        // + Volver a Cargar los Alines
    }



}
