using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.UI;
#endif
using UnityEngine;

public class ControlsBox : MonoBehaviour
{
    public Animator anim;

    public void SetAnim() {
        Debug.Log("Pressed!");
        anim.SetBool("On", !anim.GetBool("On"));
    }
}
