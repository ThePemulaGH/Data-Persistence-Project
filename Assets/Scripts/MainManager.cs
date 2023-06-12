using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        if (GameManager.instance != null)
        {
            ScoreText.text = GameManager.instance.playerName + "'s " + $"Score : {m_Points}";
            bestScoreText.text = "Best Score : " + GameManager.instance.scoreDatas[0].playerName + " : " + GameManager.instance.scoreDatas[0].score;
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        if (GameManager.instance != null) ScoreText.text = GameManager.instance.playerName + "'s " + $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        CompareBestScore();
        GameManager.instance.SaveScoreData();
        GameManager.instance.SaveSettingsData();
    }

    void CompareBestScore()
    {
        if (GameManager.instance != null)
        {
            //catat score baru
            ScoreData newScoreData = new ScoreData();
            newScoreData.playerName = GameManager.instance.playerName;
            newScoreData.difficulty = GameManager.instance.difficulty;
            newScoreData.score = m_Points;

            //kalo sudah ada highscore yang tercatat di posisi paling bawahnya
            if (GameManager.instance.scoreDatas[GameManager.instance.scoreDatas.Length - 1] != null)
            {
                //kalo score baru lebih besar daripada highscore yang paling rendah, maka score baru akan gantikan posisi highscore yang paling rendah
                if (newScoreData.score > GameManager.instance.scoreDatas[GameManager.instance.scoreDatas.Length - 1].score)
                {
                    GameManager.instance.scoreDatas[GameManager.instance.scoreDatas.Length - 1] = newScoreData;
                    Debug.Log("Switched the lowest high score");
                }
            }
            else if (GameManager.instance.scoreDatas[GameManager.instance.scoreDatas.Length - 1] == null)//kalo belum ada highscore yang tercatat di posisi paling bawahnya, langsung masukkan ke list
            {
                GameManager.instance.scoreDatas[GameManager.instance.scoreDatas.Length - 1] = newScoreData;
                Debug.Log("Instantly insert to the List because null");
            }

            

            //lanjut check bandingkan dengan highscore lebih di atasnya
            for (int i = GameManager.instance.scoreDatas.Length-1; i > 0; i--)
            {
                if (GameManager.instance.scoreDatas[i-1] != null) //kalo sudah ada highscore yang tercatat di posisi atasnya
                {
                    if (GameManager.instance.scoreDatas[i].score > GameManager.instance.scoreDatas[i-1].score) //kalo highscore ini lebih besar daripada posisi di atasnya
                    {
                        //tukar posisi
                        ScoreData tempScoreData = GameManager.instance.scoreDatas[i - 1];
                        GameManager.instance.scoreDatas[i-1] = GameManager.instance.scoreDatas[i];
                        GameManager.instance.scoreDatas[i] = tempScoreData;
                        Debug.Log("Tukar Posisi Happen on Loop" + i);
                    }
                }
                else if (GameManager.instance.scoreDatas[i-1] == null) //kalo belum ada highscore yang tercatat di posisi atasnya, langsung masukkan ke list
                {
                    GameManager.instance.scoreDatas[i] = newScoreData;
                    Debug.Log("Instantly insert to the List Happen on Loop" +  i);
                }
            }
            
        }
            
    }
}
