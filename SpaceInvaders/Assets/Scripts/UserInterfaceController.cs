using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private Text panelText;

    [SerializeField] private UILifeIndicator lifesIndicator;


    private void Start()
    {
        panelText = panel.transform.GetChild(0).GetComponent<Text>();
    }


    public void LifesToShow(int values)
    {
        lifesIndicator.ShowValue(values);
    }


    public void TurnOnPausePanel()
    {
        SetPanel(true, "Pause");
    }

    public void TurnOffPausePanel()
    {
        SetPanel(false, "Pause");
    }


    public void TurnOnGameOverPanel()
    {
        SetPanel(true, "Game Over");
    }




    private void SetPanel(bool isActive, string text)
    {
        panelText.text = text;
        panel.SetActive(isActive);
    }

}
