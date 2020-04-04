using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIMenu : MonoBehaviour
{
    [SerializeField] public GameObject options;

    private IMenuOption[] menuOptions;

    internal int currentOption = 0;

    private void Awake()
    {
        menuOptions = options.transform.GetComponentsInChildren<IMenuOption>();
    }


    private void OnEnable()
    {
        UpdateVisual();
    }


    private void OnDisable()
    {
        currentOption = 0;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AudioManager.Instance.PlayFXSound(AudioManager.FXSounds.MenuEnter);
            ChangeToNextOptionMenu();
            UpdateVisual();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AudioManager.Instance.PlayFXSound(AudioManager.FXSounds.MenuEnter);
            ChangeToPreviusOptionMenu();
            UpdateVisual();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            OptionSelected();
        }
    }


    private void ChangeToNextOptionMenu()
    {
        currentOption += 1;

        if (currentOption >= menuOptions.Length)
            currentOption = 0;
    }


    private void ChangeToPreviusOptionMenu()
    {
        currentOption -= 1;

        if (currentOption < 0)
            currentOption = menuOptions.Length - 1;
    }


    private void UpdateVisual()
    {
        for (int i = 0; i < menuOptions.Length; i++)
        {
            if (i == currentOption)
                menuOptions[i].Select();
            else
                menuOptions[i].Unselect();
        }
    }


    internal abstract void OptionSelected();
}