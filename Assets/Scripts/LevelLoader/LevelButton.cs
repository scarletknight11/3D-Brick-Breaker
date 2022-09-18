using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public Text leveltext;

    string levelload;

    public void ButtonText(string _leveltext, string _levelload)
    {
        leveltext.text = _leveltext;
        levelload = _levelload;
    }

    public void LevelLoad()
    {
        SceneManager.LoadScene(levelload);
    }

}
