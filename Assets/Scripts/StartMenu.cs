using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public TMP_InputField nameInputField;

    private void Start()
    {
        nameInputField.text = GameManager.instance.playerName;
    }

    public void UpdateName()
    {
        GameManager.instance.playerName = nameInputField.text;
    }

    public void StartGame()
    {
        if (nameInputField.text == null || nameInputField.text.Length < 1)
        {
            GameManager.instance.playerName = "Unnamed";
        }
        else if (nameInputField.text.Length > 0)
        {
            GameManager.instance.playerName = nameInputField.text;
        }
        SceneManager.LoadScene(1); //1 for Main
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene(2); //2 for Settings
    }

    public void GoToHighScores()
    {
        SceneManager.LoadScene(3); //3 for High Scores
    }

    public void QuitGame()
    {
        GameManager.instance.SaveScoreData();
        GameManager.instance.SaveSettingsData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode(); //code to exit play mode in Unity Editor
#else
        		Application.Quit(); //Original code to quit Unity player
#endif
    }
}
