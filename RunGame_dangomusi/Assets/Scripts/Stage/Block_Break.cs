using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Block_Break : MonoBehaviour
{
    GameObject playerObj;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (player.RotationMode == true) {
            Destroy(gameObject);
        }
    }
}
