using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DungeonGraphManager : MonoBehaviour
{
    public static DungeonGraphManager I { get; private set; }
    
    public static int CurrentLevel = 0;
    private static int currentDungeonDifficulty = -1;
    public UnityEvent OnVictory;
    public UnityEvent OnDefeat;
    
    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void LoadDungeon(int difficulty)
    {
        currentDungeonDifficulty = difficulty;
        SceneManager.LoadScene("DungeonLevel");
    }

    public static void OnDungeonCompleted()
    {
        if (currentDungeonDifficulty == 0)
        {
            // end game
            Debug.Log("Victory!");
            I.OnVictory.Invoke();
        }
        CurrentLevel += currentDungeonDifficulty;
        SceneManager.LoadScene("DungeonGraph");
    }
}
