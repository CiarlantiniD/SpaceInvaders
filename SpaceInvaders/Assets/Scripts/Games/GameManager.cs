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
    [SerializeField] private UserInterfaceController userInferfaceController = null;
    [SerializeField] private AlienInstancer alienInstancer = null;
    [SerializeField] private GameObject player = null;

    private PlayerController currentPlayerController;

    private SceneManager sceneManager;
    private RecordManager recordManager;

    static public Action OnPlayerDestroy;
    static public Action OnAlienDestroy;
    static public Action OnAllAliensDestroy;
    static public Action OnAliensTouchFloor;

    static public Action OnPause;
    static public Action OnUnpause;
    static public Action OnReturnToMenu;
    private bool isPaused;


    public int PlayerLifes { get; private set; } = 3;

    private int currentScore;

    private void Start()
    {
        sceneManager = SceneManager.Instance;

        if (sceneManager == null)
            throw new Exception("No se encontro el SceneManager");

        recordManager = new RecordManager();

        currentPlayerController = FindObjectOfType<PlayerController>();

        userInferfaceController.LifesToShow(PlayerLifes);

        OnPlayerDestroy += PlayerDestroy;
        OnAlienDestroy += Score;
        OnAllAliensDestroy += RestartLevel;
        OnAliensTouchFloor += ResetAndLoseALife;

        OnReturnToMenu += ReturnToMenu;

        AlienInstancerConfiguration config = new AlienInstancerConfiguration(columns, rows, pandding);
        alienInstancer.SetConfiguration(config);
        alienInstancer.CreateAliens();


        userInferfaceController.SetScore(currentScore);
    }

    private void OnDestroy()
    {
        OnPlayerDestroy -= PlayerDestroy;
        OnAlienDestroy -= Score;
        OnAllAliensDestroy -= RestartLevel;
        OnAliensTouchFloor -= ResetAndLoseALife;

        OnReturnToMenu -= ReturnToMenu;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                OnUnpause?.Invoke();
                isPaused = false;
            }
            else
            {
                OnPause?.Invoke();
                isPaused = true;
            }
                
        }
    }



    private void Score()
    {
        currentScore += 300; 
        userInferfaceController.SetScore(currentScore);
    }



    private void RestartLevel()
    {
        Debug.Log("Todos los aliens murieron. Se reinicia el nivel");
        StartCoroutine(ResetAnimation());
    }

    private IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(3);

        currentPlayerController.ResetPlayer();
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
        currentPlayerController.ResetPlayer();
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

        ReturnToMenu();
    }

    private void StopGame()
    {
        alienInstancer.Stop();
    }

    private void ReturnToMenu()
    {
        sceneManager.GoBackToMenu();
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
