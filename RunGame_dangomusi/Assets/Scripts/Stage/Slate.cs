using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slate : MonoBehaviour
{

    public string text;
    public GameObject textObj;
    Text slatetext;

    private void Start() {
        slatetext = textObj.GetComponent<Text>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            slatetext.text = " ";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            slatetext.text = text;
        }
    }
}
