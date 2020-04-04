using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject options;
    
    private IMenuOption[] menuOptions;


    private int currentState = 0;

    private void Awake()
    {
        menuOptions = options.transform.GetComponentsInChildren<UIPauseMenuOption>();
    }

    private void OnEnable()
    {
        UpdateVisual();       
    }
   
    private void OnDisable()
    {
        currentState = 0;   
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
        currentState += 1;

        if (currentState >= menuOptions.Length)
            currentState = 0;
    }

    private void ChangeToPreviusOptionMenu()
    {
        currentState -= 1;

        if (currentState < 0)
            currentState = menuOptions.Length -1;
    }

    private void UpdateVisual()
    {
        for (int i = 0; i < menuOptions.Length; i++)
        {
            if (i == currentState)
                menuOptions[i].Select();
            else
                menuOptions[i].Unselect();
        }
    }

    private void OptionSelected()
    {
        switch (currentState)
        {
            case 0: AudioManager.Instance.PlayFXSound(AudioManager.FXSounds.MenuBack); GameManager.OnUnpause?.Invoke(); break;
            case 1: GameManager.OnReturnToMenu?.Invoke(); break;
            default:
                break;
        }
    }


}