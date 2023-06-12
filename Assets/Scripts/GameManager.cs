using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int difficulty;
    public int bestScore;
    public string playerName;
    public ScoreData[] scoreDatas;


    
    void Awake()
    {
        //default settings
        difficulty = 1;
        playerName = string.Empty;

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Game Manager created"); //info to whether has been created or not
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettingsData();
            LoadScoreData();
        }
        
    }

    [System.Serializable]
    class SaveData //settings data
    {
        public int difficulty;
        public string playerName;
    }

    public void SaveSettingsData()
    {
        SaveData data = new SaveData();
        data.difficulty = difficulty;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        
        string path = Application.persistentDataPath + "/settingfile.json"; //save file ke C:\Users\[user]\AppData\LocalLow\Unity 
        File.WriteAllText(path, json);
        if (File.Exists(path)) //check sudah kesave atau belum dengan ada filenya atau tidak di lokasi save
        {
            Debug.Log("Settings file successfully saved to " + path);
        }
        else Debug.Log("Failed to save");
    }

    public void LoadSettingsData()
    {
        string path = Application.persistentDataPath + "/settingfile.json"; //path yang akan di-load
        if (File.Exists(path)) //check apa ada filenya atau tidak
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            difficulty = data.difficulty;
            playerName = data.playerName;
        }
        else Debug.Log("File doesn't exist or is missing");
    }

    
    

    

    public void SaveScoreData()
    {
        if (scoreDatas != null) 
        {
            for (int i = 0; i < scoreDatas.Length; i++)
            {
                ScoreData data = new ScoreData();
                data = scoreDatas[i];

                string json = JsonUtility.ToJson(data);

                string path = Application.persistentDataPath + $"/scorefile{i}.json"; //save file ke C:\Users\[user]\AppData\LocalLow\Unity
                File.WriteAllText(path, json);
                if (File.Exists(path)) //check sudah kesave atau belum dengan ada filenya atau tidak di lokasi save
                {
                    Debug.Log($"Score file {i} successfully saved to " + path);
                }
                else Debug.Log("Failed to save");
            }
            
        }
        
    }

    public void LoadScoreData()
    {
        if (scoreDatas != null)
        {
            for (int i = 0; i < scoreDatas.Length; i++)
            {
                string path = Application.persistentDataPath + $"/scorefile{i}.json"; //path yang akan di-load
                if (File.Exists(path)) //check apa ada filenya atau tidak
                {
                    string json = File.ReadAllText(path);
                    ScoreData data = JsonUtility.FromJson<ScoreData>(json);

                    scoreDatas[i] = data;
                }
                else Debug.Log($"Score file {i} doesn't exist or is missing");
            }
        }
        
    }

    

}
