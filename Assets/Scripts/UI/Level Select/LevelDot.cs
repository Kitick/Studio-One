
using TMPro;
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

        lockedText.SetActive(false); // make invisible
        playableText.SetActive(false); // make invisible
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

        GetComponent<AudioSource>().Play();

        if (selectable == false && active == false) {
            lockedText.SetActive(true);
            lockedText.GetComponent<Animation>()["Hovered"].speed = 1.0f;
            lockedText.GetComponent<Animation>().Play();
        } else if (selectable == true && active == false) {
            playableText.SetActive(true);
        }

        active = true;
    }

    public void HoverOff() {

        if (selectable == false && active == true) {
            lockedText.GetComponent<Animation>()["Hovered"].speed = -1.0f;
            lockedText.GetComponent<Animation>().Play();
            lockedText.SetActive(false);
        } else if (selectable == true && active == true) {
            playableText.SetActive(false);
        }

        active = false;
    }

    public void Unlock() {
        selectable = true;
        dotSprite.color = Color.green;
    }
}
