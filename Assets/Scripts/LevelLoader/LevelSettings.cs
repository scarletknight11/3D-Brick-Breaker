using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour {

    public GameObject levels;

    public string[] levelsToLoad;

    public Transform grid;

    void Start()
    {
        LevelsLoaded();
    }

    void LevelsLoaded()
    {
        for (int i = 0; i < levelsToLoad.Length; i++)
        {
            GameObject button = Instantiate(levels);
            button.GetComponent<LevelButton>().ButtonText((i + 1).ToString(),levelsToLoad[i]);

            button.transform.SetParent(grid, false);
        }
    }

}
