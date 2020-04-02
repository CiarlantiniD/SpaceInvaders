using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private enum GameScene { Menu = 0, Game}

    private GameScene currentGameScene;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        currentGameScene = GameScene.Menu;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GoToGame();
        }
    }

    public void GoToGame()
    {
        if (currentGameScene == GameScene.Menu)
        {
            currentGameScene = GameScene.Game;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }


    public void GoBackToMenu()
    {
        if (currentGameScene == GameScene.Game)
        {
            currentGameScene = GameScene.Menu;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }


}
