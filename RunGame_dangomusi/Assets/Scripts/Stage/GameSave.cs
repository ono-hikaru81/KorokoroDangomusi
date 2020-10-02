using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Rungame.Stage {
    public class GameSave : MonoBehaviour {

        void OnTriggerEnter2D(Collider2D collision) {
            if (collision.tag == "Player") {
                PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
                PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
                PlayerPrefs.Save();
            }
        }
    }
}