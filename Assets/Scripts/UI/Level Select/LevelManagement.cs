using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public LevelDot level1;
    public LevelDot level2;
    public LevelDot level3;


    // Start is called before the first frame update
    void Start()
    {
        
        level1.selectable = (PlayerPrefs.GetInt("LevelOne", 0)) != 0;
        level2.selectable = (PlayerPrefs.GetInt("LevelTwo", 0)) != 0;
        level3.selectable = (PlayerPrefs.GetInt("LevelThree", 0)) != 0;

    }

}
