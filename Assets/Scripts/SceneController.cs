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
        if (instance != null)
            Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);    
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //레벨 변경시 호출
    }
}
