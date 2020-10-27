using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Door : MonoBehaviour
{
    Vector2 pos;
    Vector2 playerPos;

    bool openTrigger = false;
    bool moveTrigger = false;
    bool closeTrigger = false;
    bool activeTrigger = true;

    GameObject playerObj;
    Player player;

    public AudioClip SE_door;

    private void Start() {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
    }

    private void FixedUpdate() {
        
        if (openTrigger) {
            transform.Translate(0.0f, 0.05f, 0.0f);
            if(transform.position.y >= pos.y + 3.0f) {
                openTrigger = false;
                moveTrigger = true;
            }
        }
        else if (moveTrigger) {
            player.transform.Translate(0.1f, 0, 0, Space.World);
            if (player.transform.position.x >= pos.x + 1.0f) {
                moveTrigger = false;
                closeTrigger = true;
            }
        }
        else if (closeTrigger) {
            transform.Translate(0.0f, -0.05f, 0.0f);
            if (transform.position.y <= pos.y) {
                closeTrigger = false;
                player.rigidbody.bodyType = RigidbodyType2D.Dynamic;
                player.invincible = false;
                GetComponent<AudioSource>().Stop();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (activeTrigger) {
                GetComponent<AudioSource>().clip = SE_door;
                GetComponent<AudioSource>().Play();
                player.invincible = true;
                player.rigidbody.bodyType = RigidbodyType2D.Static;
                pos = transform.position;
                playerPos = player.transform.position;
                openTrigger = true;
                activeTrigger = false;
            }
        }
    }
}
