using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;
    public static SceneController Instance
    {
        get
        {
            if (instance != null)
                return instance;
            return null;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    public void OnClickStartButton(string sceneName)
    {
        AudioManager.Instance.Play("Select");
        LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);    
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainGame")
        {
            CharacterPeek.Instance.UsedCharacter.gameObject.AddComponent<Player>();
            CharacterPeek.Instance.UsedCharacter.gameObject.AddComponent<PlayerController>();
        }
    }
}
