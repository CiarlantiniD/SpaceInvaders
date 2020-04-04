using System.Collections;
using UnityEngine;
using System;

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuUserInterfaceController userInterface = null;

    private SceneManager sceneManager;
    private RecordManager recordManager;

    public Action OnStartGame;
    public Action OnExitGame;

    private bool isGameStarting = false;

    private void Awake()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        recordManager = new RecordManager();

        if (sceneManager == null)
            throw new Exception("No se encontro el SceneManager");

        OnStartGame += StartGame;
        OnExitGame += ExitGame;

        SetBestScore();
    }


    private void OnDestroy()
    {
        OnStartGame -= StartGame;
        OnExitGame -= ExitGame;
    }


    private void SetBestScore()
    {
        int score = recordManager.GetBestScore();

        if (score > 0)
            userInterface.SetBestScore(score);
    }


    private void StartGame()
    {
        if (isGameStarting)
            return;

        isGameStarting = true;
        StartCoroutine(GoToGameAnimation());
    }

    private IEnumerator GoToGameAnimation()
    {
        userInterface.TriggerStartTextAnimation();
        AudioManager.Instance.PlayFXSound(AudioManager.FXSounds.MenuStarGame);
        yield return new WaitForSeconds(1f);
        userInterface.TurnOff();
        yield return new WaitForSeconds(0.5f);
        sceneManager.GoToGame();
    }


    private void ExitGame()
    {
#if UNITY_EDITOR
        Debug.Log("<color=red>ExitGame</color>");
#else
        Application.Quit();
#endif
    }

}
