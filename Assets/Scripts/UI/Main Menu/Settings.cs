using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{

    public TMP_Dropdown resolutions;
    public Slider sfxSlider;
    public Slider musicSlider;
    public AudioMixer aMixer;

    public void SetResolution() {

        switch (resolutions.value) {
            case 0:
                Screen.SetResolution(1280, 720, false);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, false);
                break;
            case 2:
                Screen.SetResolution(3840, 2160, false);
                break;
            default:
                Debug.Log("Invalid index");
                break;
        }
    }

    public void ChangeSFXVolume() {
        aMixer.SetFloat("SFXVolume", sfxSlider.value);
    }

    public void ChangeMusicVolume() {
        aMixer.SetFloat("MusicVolume", musicSlider.value);
    }


}
