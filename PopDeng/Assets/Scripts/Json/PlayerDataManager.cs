using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public bool isEndlessmode;
    public bool unlockEndless;
    public int scores;

    public PlayerData(bool isEndlessmode, bool unlockEndless, int scores)
    {
        this.isEndlessmode = isEndlessmode;
        this.unlockEndless = unlockEndless;
        this.scores = scores;
    }
}

public class PlayerDataManager : JsonManager<PlayerData>
{
    public static PlayerDataManager Instance { get; private set; }
    private PlayerData playerData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        InitializePaths("playerData.json");

        if (File.Exists(persistentPath))
        {
            LoadPlayerData(persistentPath);
        }
        else
        {
            if (File.Exists(streamingAssetsPath))
            {
                File.Copy(streamingAssetsPath, persistentPath);
                LoadPlayerData(persistentPath);
                Debug.Log("Copied default player data to Persistent Data Path.");
            }
            else
            {
                Debug.LogError("Default player data not found in StreamingAssets.");
            }
        }

    }

    public void LoadPlayerData(string path)
    {
        string jsonData = LoadJson(path);

        if (!string.IsNullOrEmpty(jsonData))
        {
            playerData = JsonUtility.FromJson<PlayerData>(jsonData);
            Debug.Log("Loaded player data: " + jsonData);
        }
        else
        {
            Debug.LogError("Failed to load player data: No data found.");
        }
    }

    public void SavePlayerData()
    {
        string jsonData = JsonUtility.ToJson(playerData, true);
        SaveJson(jsonData);
    }

    public void DeletePlayerData()
    {
        DeleteJson();

        if (File.Exists(persistentPath))
        {
            LoadPlayerData(persistentPath);
        }
        else
        {
            if (File.Exists(streamingAssetsPath))
            {
                File.Copy(streamingAssetsPath, persistentPath);
                LoadPlayerData(persistentPath);
                Debug.Log("Copied default player data to Persistent Data Path.");
            }
            else
            {
                Debug.LogError("Default player data not found in StreamingAssets.");
            }
        }

        //InitializePaths("playerData.json");
    }

    public bool GetIsEndlessmode()
    {
        return playerData != null ? playerData.isEndlessmode : false;
    }

    public bool GetUnlockEndless()
    {
        return playerData != null ? playerData.unlockEndless : false;
    }

    public int GetScores()
    {
        return playerData != null ? playerData.scores : 0;
    }

   
    public void UpdateIsEndlessmode(bool value)
    {
        if (playerData != null)
        {
            playerData.isEndlessmode = value;
            Debug.Log($"Updated isEndlessmode to: {value}");
            //SavePlayerData();
        }
    }

    public void UpdateUnlockEndless(bool value)
    {
        if (playerData != null)
        {
            playerData.unlockEndless = value;
            Debug.Log($"Updated unlockEndless to: {value}");
            //SavePlayerData();
        }
    }

    public void UpdateScores(int score)
    {
        if (playerData != null)
        {
            playerData.scores = score;
            Debug.Log($"Updated Scores to: {score}");
            //SavePlayerData();
        }
    }

    public PlayerData GetPlayerData() => playerData;
}
