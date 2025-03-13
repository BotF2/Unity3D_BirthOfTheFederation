using UnityEngine;
using Assets.Core;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void LoadCombatScene()
    {
        // not working yet.... SceneTransition.Instance.LoadCombatScene();
        SceneManager.LoadSceneAsync("CombatScene");
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
