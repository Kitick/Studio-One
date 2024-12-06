using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public string levelName;

    void LevelUpdate() {
        if (levelName == "LevelOne") {
            PlayerPrefs.SetInt("LevelTwo", 1);
        } else if (levelName == "LevelTwo") {
            PlayerPrefs.SetInt("LevelThree", 1);
        } else if (levelName == "LevelThree") {
            PlayerPrefs.SetInt("LevelTwo", 1); // just to have something for lvl three
        } else {
            Debug.Log("End!");
        }
    }
}
