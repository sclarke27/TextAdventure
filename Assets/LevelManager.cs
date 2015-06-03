using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public void LoadLevel(string levelName)
    {
        Debug.Log("Load Level:" + levelName);
        Application.LoadLevel(levelName);
    }

    public void QuitRequest()
    {
        Application.Quit();
    }
}
