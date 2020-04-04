using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] 
    private GameObject panel;
    private Text panelText;

    [SerializeField] private UILifeIndicator lifesIndicator;

    [SerializeField] private Text scoreText;

    private void Start()
    {
        panelText = panel.transform.GetChild(0).GetComponent<Text>();

        GameManager.OnPause += ActivePauseMenu;
        GameManager.OnUnpause += DesactivePauseMenu;
    }

    private void OnDestroy()
    {
        GameManager.OnPause -= ActivePauseMenu;
        GameManager.OnUnpause -= DesactivePauseMenu;
    }


    public void LifesToShow(int values)
    {
        lifesIndicator.ShowValue(values);
    }


    public void TurnOnGameOverPanel()
    {
        SetPanel(true, "Game Over");
    }


    public void SetScore(int score)
    {
        scoreText.text = "Score - " + score.ToString("00000000000");
    }

    public void ActivePauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void DesactivePauseMenu()
    {
        pauseMenu.SetActive(false);
    }


    private void SetPanel(bool isActive, string text)
    {
        panelText.text = text;
        panel.SetActive(isActive);
    }

}
