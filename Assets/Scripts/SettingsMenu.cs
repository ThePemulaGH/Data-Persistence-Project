using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    int difficulty;
    public Button[] difficultyButton;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
        {
            difficulty = GameManager.instance.difficulty;
        }
        else { difficulty = 1; }
        ChangeButtonAppearance();
    }


    public void SetDifficulty(int diff)
    {
        difficulty = diff;
        if (GameManager.instance != null)
        {
            GameManager.instance.difficulty = difficulty;
        }
        ChangeButtonAppearance();
    }

    void ChangeButtonAppearance()
    {
        foreach (Button difButton in difficultyButton)
        {
            difButton.interactable = true;
        }
        switch (difficulty)
        {
            case 1:
                difficultyButton[0].interactable = false; 
                break;

            case 2:
                difficultyButton[1].interactable = false;
                break;

            default:
                difficultyButton[2].interactable = false;
                break;
        }
    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
