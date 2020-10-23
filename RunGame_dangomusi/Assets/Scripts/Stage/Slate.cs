using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slate : MonoBehaviour
{

    public string text;
    public GameObject textObj;
    Text slatetext;

    public Image popupWindow;

    float timer;

    private void Start() {
        slatetext = textObj.GetComponent<Text>();
    }

    private void Update() {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Return)) {
            slatetext.text = " ";
            popupWindow.color = new Color( 0.0f, 0.0f, 0.0f, 0.0f );
            Time.timeScale = 1.0f;
            timer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if ( timer >= 1.0f ) {
            if ( collision.tag == "Player" ) {
                StartCoroutine( "DeleteText" );
                slatetext.text = text;
                popupWindow.color = new Color( 0.0f, 0.0f, 0.0f, 0.5f );
                Time.timeScale = 0.01f;
            }
        }
    }

    IEnumerator DeleteText() {
        yield return new WaitForSeconds( 0.07f );
        slatetext.text = " ";
        popupWindow.color = new Color( 0.0f, 0.0f, 0.0f, 0.0f );
        Time.timeScale = 1.0f;
        timer = 0;
    }
}
