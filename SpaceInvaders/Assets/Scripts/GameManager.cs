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
    [SerializeField] private UserInterfaceController userInferfaceController;
    [SerializeField] private AlienInstancer alienInstancer;
    [SerializeField] private GameObject player;

    private PlayerController currentPlayerController;

    private SceneManager sceneManager;
    private RecordManager recordManager;

    static public Action OnPlayerDestroy;
    static public Action OnAlienDestroy;
    static public Action OnAllAliensDestroy;
    static public Action OnAliensTouchFloor;
    

    public int PlayerLifes { get; private set; } = 3;

    private int currentScore;

    private void Start()
    {
        sceneManager = SceneManager.Instance; //FindObjectOfType<SceneManager>();

        if (sceneManager == null)
            throw new Exception("No se encontro el SceneManager");

        recordManager = new RecordManager();

        userInferfaceController.LifesToShow(PlayerLifes);

        OnPlayerDestroy += PlayerDestroy;
        OnAlienDestroy += delegate { currentScore += 300; userInferfaceController.SetScore(currentScore); };
        
        OnAllAliensDestroy += RestartLevel;
        OnAliensTouchFloor += ResetAndLoseALife;

        AlienInstancerConfiguration config = new AlienInstancerConfiguration(columns, rows, pandding);
        alienInstancer.SetConfiguration(config);
        alienInstancer.CreateAliens();


        userInferfaceController.SetScore(currentScore);

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

        userInferfaceController.LifesToShow(PlayerLifes);

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
        yield return new WaitForSeconds(1f);

        userInferfaceController.TurnOnGameOverPanel();
        yield return new WaitForSeconds(4);

        if (recordManager.GetBestScore() < currentScore)
            recordManager.SaveBestScore(currentScore);

        sceneManager.GoBackToMenu();
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
