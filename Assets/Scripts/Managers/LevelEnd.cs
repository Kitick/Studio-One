using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string levelName;
    public GameObject[] children;

    GameObject[] enemies;

    void Start() {
        foreach (GameObject child in children) {
            child.SetActive(false);
        }
    }

    void Update() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies == null || enemies.Length == 0) {
            foreach (GameObject child in children) {
                child.SetActive(true);
            }
        }
    }

    public void LevelUpdate() {
        Debug.Log("Help!");
        if (levelName == "LevelOne") {
            PlayerPrefs.SetInt("LevelTwo", 1);
        } else if (levelName == "LevelTwo") {
            PlayerPrefs.SetInt("LevelThree", 1);
        } else if (levelName == "LevelThree") {
            PlayerPrefs.SetInt("LevelTwo", 1); // just to have something for lvl three
        } else {
            Debug.Log("End!");
        }

        SceneManager.LoadScene("Assets/Scenes/Level Select/LevelSelect.unity");
    }
}
