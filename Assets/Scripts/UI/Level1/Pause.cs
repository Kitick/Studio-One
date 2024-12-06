using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
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
        }
        
    }

    public void GoToMenu() {
        SceneManager.LoadScene("Assets/Scenes/Main Menu/MainMenu.unity");
    }
}
