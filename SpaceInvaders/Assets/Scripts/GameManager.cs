using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int columns = 10;
    [SerializeField] private int rows = 4;
    [SerializeField] private float pandding = 0.25f;
    [Space(10)]
    [SerializeField] private UserInterfaceController UIController;
    [SerializeField] private AlienInstancer alienInstancer;
    [SerializeField] private GameObject player;


    static public Action OnPlayerDestroy;
    static public Action OnAllAliensDestroy;

    public int PlayerLifes { get; private set; } = 3;

    private void Start()
    {
        UIController.LifesToShow(PlayerLifes);

        OnPlayerDestroy += PlayerDestroy;
        OnAllAliensDestroy += RestartLevel;

        AlienInstancerConfiguration config = new AlienInstancerConfiguration(columns, rows, pandding);
        alienInstancer.SetConfiguration(config);
        alienInstancer.CreateAliens();
    }




    private void RestartLevel()
    {
        Debug.Log("Todos los aliens murieron. Se reinicia el nivel");
        StartCoroutine(ResetAnimation());
    }

    private IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(2);

        // + Animacion
        // + Reposicionar Player
        alienInstancer.OnReset();
    }




    private void PlayerDestroy()
    {
        PlayerLifes -= 1;

        UIController.LifesToShow(PlayerLifes);

        if (PlayerLifes > 0)
            StartCoroutine(PlayerRecoveryAnimation());
        else
            StartCoroutine(StopGameAnimation());
    }




    IEnumerator PlayerRecoveryAnimation()
    {
        yield return new WaitForSeconds(2);
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Instantiate(player);
    }

    



    IEnumerator StopGameAnimation()
    {
        Debug.Log("Game Over");
        StopGame();
        yield return new WaitForSeconds(2);
        // + Vuelve al Menu
    }

    private void StopGame()
    {
        
    }



}
