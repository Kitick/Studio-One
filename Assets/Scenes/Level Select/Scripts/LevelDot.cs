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
    public GameObject lockedText;
    public string levelName;
    public bool selectable = false;

    // Start is called before the first frame update
    void Start()
    {
        if (selectable == false) {
            dotSprite.color = Color.red;
        } else if (selectable == true) {
            dotSprite.color = Color.green;
        }

        Debug.Log(lockedText.GetComponent<TextMeshPro>().material.name);
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
}
