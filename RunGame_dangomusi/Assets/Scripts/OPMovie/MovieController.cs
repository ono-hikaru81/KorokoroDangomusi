using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovieController : MonoBehaviour {
    public Sprite[] sprite;
    public Image imageObj;

    float timeCounter = 4.5f;
    float alpha = 1.0f;

    int spriteCount = 0;

    enum FadeMode {
        None,
        In,
        Out
    };
    FadeMode fademode = FadeMode.None;

    private void FixedUpdate () {
        timeCounter += Time.deltaTime;

        if ( fademode == FadeMode.In ) {
            imageObj.sprite = sprite [ spriteCount ];
            if ( FadeIn() == true ) {
                fademode = FadeMode.None;
                spriteCount += 1;
                timeCounter = 0;
            }
        }
        else if ( fademode == FadeMode.Out ) {
            if ( FadeOut() == true ) {
                if ( spriteCount >= sprite.Length ) {
                    SceneManager.LoadScene( "Stage 0" );
                }
                else {
                    fademode = FadeMode.In;
                }
            }
        }

        if ( timeCounter >= 5.0f ) {
            fademode = FadeMode.Out;
            timeCounter = 0;
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
