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

    private PlayerController currentPlayerController;

    static public Action OnPlayerDestroy;
    static public Action OnAllAliensDestroy;
    static public Action OnAliensTouchFloor;

    public int PlayerLifes { get; private set; } = 3;

    private void Start()
    {
        UIController.LifesToShow(PlayerLifes);

        OnPlayerDestroy += PlayerDestroy;
        OnAllAliensDestroy += RestartLevel;
        OnAliensTouchFloor += ResetAndLoseALife;

        AlienInstancerConfiguration config = new AlienInstancerConfiguration(columns, rows, pandding);
        alienInstancer.SetConfiguration(config);
        alienInstancer.CreateAliens();

        CreatePlayer();
    }



    private void RestartLevel()
    {
        Debug.Log("Todos los aliens murieron. Se reinicia el nivel");
        StartCoroutine(ResetAnimation());
    }

    private IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(3);

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
        GameObject currentPlayer = Instantiate(player);
        currentPlayerController = currentPlayer.GetComponent<PlayerController>();
    }

    



    IEnumerator StopGameAnimation()
    {
        Debug.Log("Game Over");
        StopGame();
        // Mustra que perdiste
        yield return new WaitForSeconds(2);
        // + Vuelve al Menu
    }

    private void StopGame()
    {
        alienInstancer.Stop();
    }




    private void ResetAndLoseALife() 
    {
        if (currentPlayerController == null)
            return;
        
        PlayerLifes = 1;
        currentPlayerController.Destroy();
        currentPlayerController = null;
    }





}
