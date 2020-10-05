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

    //에디터에서 버튼의 OnClick으로 호출
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);    
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainGame")
        {
            CharacterManager.Instance.UsedCharacter.gameObject.AddComponent<Player>();
            CharacterManager.Instance.UsedCharacter.gameObject.AddComponent<PlayerController>();
        }
    }
}
