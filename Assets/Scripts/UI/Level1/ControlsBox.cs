using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.UI;
#endif
using UnityEngine;

public class ControlsBox : MonoBehaviour
{

    public GameObject self;

    public void SetAnim() {
        Debug.Log("Pressed!");
        self.SetActive(false);
    }
}
