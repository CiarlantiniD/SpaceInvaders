using UnityEngine;

public class UIPauseMenuController : UIMenu
{
    internal override void OptionSelected()
    {
        switch (currentOption)
        {
            case 0: AudioManager.Instance.PlayFXSound(AudioManager.FXSounds.MenuBack); GameManager.OnUnpause?.Invoke(); break;
            case 1: GameManager.OnReturnToMenu?.Invoke(); break;
            default:
                break;
        }
    }
}