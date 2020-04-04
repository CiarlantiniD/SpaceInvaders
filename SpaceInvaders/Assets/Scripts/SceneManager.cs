using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instanse;

    public static SceneManager Instance
    {
        get
        {
            if (instanse == null)
                instanse = FindObjectOfType<SceneManager>();

            return instanse;
        }
    }


    private enum GameScene { Menu = 0, Game}

    private GameScene currentGameScene;

    private void Awake()
    {
        if (instanse == null)
        {
            instanse = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this);

        currentGameScene = GameScene.Menu;
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
