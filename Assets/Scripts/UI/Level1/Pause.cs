using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.UI;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    GameObject[] uiStuff;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);

        uiStuff = GameObject.FindGameObjectsWithTag("LevelUI");
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            foreach (GameObject ui in uiStuff) {
                ui.SetActive(!ui.activeSelf);
            }

            pauseMenu.SetActive(!pauseMenu.activeSelf);

            if (pauseMenu.activeSelf) {
                Time.timeScale = 0f;
            } else {
                Time.timeScale = 1f;
            }
        }
        
    }

    public void GoToMenu() {
        Time.timeScale = 1f;
        Debug.Log("Pressed!");
        SceneManager.LoadScene("Assets/Scenes/Main Menu/MainMenu.unity");
    }

    public void GoToMap() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Assets/Scenes/Level Select/LevelSelect.unity");
    }
}
