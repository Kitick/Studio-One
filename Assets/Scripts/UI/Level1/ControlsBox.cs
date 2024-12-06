using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class ControlsBox : MonoBehaviour
{
    public Animator anim;

    public void setAnim() {
        anim.SetBool("On", !anim.GetBool("On"));
    }
}
