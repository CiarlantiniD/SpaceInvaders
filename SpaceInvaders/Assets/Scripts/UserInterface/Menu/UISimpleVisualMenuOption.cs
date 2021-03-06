﻿using UnityEngine;
using UnityEngine.UI;

public class UISimpleVisualMenuOption : MonoBehaviour, IMenuOption
{
    [SerializeField] private Text optionText = null;

    private Color colorSelect = Color.white;
    private Color colorUnselect = Color.gray;

    public void Select()
    {
        optionText.color = colorSelect;
    }

    public void Unselect()
    {
        optionText.color = colorUnselect;
    }
}
