using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    public void Play() {
        SceneManager.LoadScene("Assets/Scenes/Level Select/LevelSelect.unity");
    }

    public void Settings() {
        Debug.Log("Settings opened");
    }

    public void Quit() {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
