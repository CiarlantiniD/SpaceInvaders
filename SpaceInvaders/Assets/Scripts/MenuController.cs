using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuUserInterfaceController userInterface;

    private SceneManager sceneManager;

    private void Awake()
    {
        sceneManager = FindObjectOfType<SceneManager>();

        if (sceneManager == null)
            throw new Exception("No se encontro el SceneManager");

    }


    void Start()
    {

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
