using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuController : UIMenu
{
    internal override void OptionSelected()
    {
        switch (currentOption)
        {
            case 0: FindObjectOfType<MenuController>().OnStartGame?.Invoke(); break;
            case 1: FindObjectOfType<MenuController>().OnExitGame?.Invoke(); break;
            default:
                break;
        }
    }
}
