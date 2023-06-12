using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresScreen : MonoBehaviour
{
    public GameObject scoresPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
        {
            for (int i = 0; i < GameManager.instance.scoreDatas.Length; i++)
            {
                Vector3 offset = new Vector3(0, -75f * i, 0);
                GameObject tempScore = Instantiate(scoresPrefab, transform.position + offset, scoresPrefab.transform.rotation, transform);
                ScorePrefab tempScorePrefab = tempScore.GetComponent<ScorePrefab>();
                tempScorePrefab.positionText.text = (i+1).ToString();
                tempScorePrefab.playerNameText.text = GameManager.instance.scoreDatas[i].playerName;
                tempScorePrefab.scoreText.text = GameManager.instance.scoreDatas[i].score + "";
                tempScorePrefab.difficulty = GameManager.instance.scoreDatas[i].difficulty;
            }
        }
        else
        {
            /*for (int i = 0; i < 3; i++)
            {
                Vector3 offset = new Vector3 (0, -75f * i, 0);
                GameObject tempScore = Instantiate(scoresPrefab, transform.position + offset, scoresPrefab.transform.rotation, transform);
                ScorePrefab tempScorePrefab = tempScore.GetComponent<ScorePrefab>();
                tempScorePrefab.positionText.text = (i + 1).ToString();
                tempScorePrefab.playerNameText.text = "Unnamed";
                tempScorePrefab.scoreText.text = "0";
                tempScorePrefab.difficulty = 3;
            }*/
        }
    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
