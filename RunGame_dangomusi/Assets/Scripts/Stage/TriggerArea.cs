using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    GameObject playerObj;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // 敵との接触判定
        if (collider.tag == "Enemy")
        {
            if (player.RotationMode == false)
            {
                GameObject obj = GameObject.Find("Player");
                Destroy(obj);
                SceneController.Instance.GameOver();
            }
        }
    }
}
