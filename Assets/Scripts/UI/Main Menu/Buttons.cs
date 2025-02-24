using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    public Animator settingAnim;

    public void Play() {
        PlayerPrefs.SetInt("LevelOne", 1);
        PlayerPrefs.SetInt("LevelTwo", 0);
        PlayerPrefs.SetInt("LevelThree", 0);

        SceneManager.LoadScene("Assets/Scenes/Level Select/LevelSelect.unity");
    }

    public void Settings() {
        settingAnim.SetBool("Settings", true);
    }

    public void Quit() {
        Debug.Log("Quit!");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
# else
        Application.Quit();
#endif
    }

    public void Back() {
        settingAnim.SetBool("Settings", false);
    }
}
