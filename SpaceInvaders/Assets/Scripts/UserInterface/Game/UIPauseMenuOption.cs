using UnityEngine;
using UnityEngine.UI;

public class UIPauseMenuOption : MonoBehaviour, IMenuOption
{
    [SerializeField] private Text optionText;

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
