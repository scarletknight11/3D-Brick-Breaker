using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour {

    public GameObject levels;

    public string[] levelsToLoad;

    public Transform grid;

    public void LevelsLoaded()
    {
        for (int i = 1; i < levelsToLoad.Length; i++)
        {
            GameObject button = Instantiate(levels);
            button.GetComponent<LevelButton>();
        }
    }

}
