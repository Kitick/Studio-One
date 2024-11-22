using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelDot : MonoBehaviour
{

    public Image dotSprite;
    public GameObject  lockedText;
    public GameObject playableText;
    public string levelName;
    public bool selectable = false;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        if (selectable == false) { // dot color management
            dotSprite.color = Color.red;
        } else if (selectable == true) {
            dotSprite.color = Color.green;
        }

        lockedText.GetComponent<TextMeshProUGUI>().alpha = 0f; // make invisible
        playableText.GetComponent<TextMeshProUGUI>().alpha = 0f; // make invisible
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click() {
        if (selectable == true) {
            SceneManager.LoadScene(levelName);
        }
    }

    public void Hover() {

        this.GetComponent<AudioSource>().Play();

        if (selectable == false && active == false) {
            lockedText.GetComponent<Animation>()["Hovered"].speed = 1.0f;
            lockedText.GetComponent<Animation>().Play();
        } else if (selectable == true && active == false) {
            playableText.GetComponent<Animation>()["Hovered"].speed = 1.0f;
            playableText.GetComponent<Animation>().Play();
        }

        active = true;
    }

    public void HoverOff() {

        if (selectable == false && active == true) {
            lockedText.GetComponent<Animation>()["Hovered"].speed = -1.0f;
            lockedText.GetComponent<Animation>().Play();
        } else if (selectable == true && active == true) {
            playableText.GetComponent<Animation>()["Hovered"].speed = -1.0f;
            playableText.GetComponent<Animation>().Play();
        }

        active = false;
    }
}
