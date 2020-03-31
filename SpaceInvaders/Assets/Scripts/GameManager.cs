using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UserInterfaceController UIController;

    public int PlayerLifes { get; private set; } = 3;

    private void Start()
    {
        UIController.LifesToShow(PlayerLifes);
    }
}
