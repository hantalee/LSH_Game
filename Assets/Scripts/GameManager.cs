using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void PlayerEventHandler();
    public static event PlayerEventHandler OnPlayerDead;

    private static GameManager instance;
    public static GameManager Instance
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
        }
    }

    public void PlayerDead()
    {
        OnPlayerDead();
        SkillManager.Instance.DeactivateAllUsedSkills();
        LoadLobbyScene();
    }

    public void LoadLobbyScene()
    {
        SceneController.Instance.LoadScene("Lobby");
    }

    public void OnClickExistButton()
    {
        Application.Quit();
    }
}
