using UnityEngine;
using TMPro;

public class ScorePrefab : MonoBehaviour
{
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI difficultyText;
    public int difficulty;

    private void Start()
    {
        switch (difficulty)
        {
            case 1:
                difficultyText.text = "Normal";
                break;

            case 2:
                difficultyText.text = "Hard";
                break;

            default:
                difficultyText.text = "Harder";
                break;
        }
    }
}
