using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuUserInterfaceController userInterface;

    private SceneManager sceneManager;
    private RecordManager recordManager;

    private void Awake()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        recordManager = new RecordManager();

        if (sceneManager == null)
            throw new Exception("No se encontro el SceneManager");

        SetBestScore();
    }

    private void SetBestScore()
    {
        int score = recordManager.GetBestScore();

        if (score > 0)
            userInterface.SetBestScore(score);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(GoToGameAnimation());
        }
    }

    private IEnumerator GoToGameAnimation()
    {
        userInterface.TriggerStartTextAnimation();
        yield return new WaitForSeconds(1f);
        userInterface.TurnOff();
        yield return new WaitForSeconds(0.5f);
        sceneManager.GoToGame();
    }



}
