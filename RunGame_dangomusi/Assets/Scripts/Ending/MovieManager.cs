using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovieManager : MonoBehaviour {
    public Sprite[] sprite;
    public Image imageObj;

    float timeCounter = 0;
    float alpha = 0.0f;

    bool endFadeIn = false;
    bool endFadeOut = true;

    int spriteCount = 0;

    public AudioClip endingBgm;

    private void Start () {
        imageObj.sprite = sprite[spriteCount];

        var bgmAudio = Camera.main.GetComponent<AudioSource>();
        bgmAudio.clip = endingBgm;
        bgmAudio.Play();
    }

    private void FixedUpdate () {

        if ( endFadeIn == false ) {
            if ( FadeIn() == true ) {
                endFadeIn = true;
            }
        }
        else if ( endFadeOut == false ) {
            if ( FadeOut() == true ) {
                endFadeOut = true;
                SceneManager.LoadScene( "Credit" );
            }
        }
        else {
            timeCounter += Time.deltaTime;
        }

        if ( spriteCount >= sprite.Length ) {
            if ( timeCounter >= 3.0f ) {
                endFadeOut = false;
            }
        }
        else if ( timeCounter >= 0.5f ) {
            timeCounter = 0;
            imageObj.sprite = sprite[spriteCount];
            spriteCount += 1;
        }
    }

    bool FadeIn () {
        alpha += 1.0f / 60.0f;
        imageObj.color = new Color( 1.0f, 1.0f, 1.0f, alpha );
        if ( alpha > 1.0f ) {
            return true;
        }
        return false;
    }

    bool FadeOut () {
        alpha -= 1.0f / 60.0f;
        imageObj.color = new Color( 1.0f, 1.0f, 1.0f, alpha );
        if ( alpha < 0.0f ) {
            return true;
        }
        return false;
    }
}
