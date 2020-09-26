using Microsoft.Unity.VisualStudio.Editor;
using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// <summary>
/// 敵の『アリ』を表します。
/// </summary>
public class Ants : MonoBehaviour
{
    GameObject playerObj;

    Player player;

    public enum ActionPart
    {
        Wait, // 待機モーション
        Raid, // 戦闘モーション
        Death // 死亡モーション
    }
    ActionPart Action = ActionPart.Wait;

    // アリの速度
    float speed_x = -4;
    float speed_y = 0.0f;
    float speed_z = 0.0f;

    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Action)
        {
            case ActionPart.Wait:
                WaitAction();
                break;
            case ActionPart.Raid:
                RaidAction();
                break;
            case ActionPart.Death:
                DeathAction();
                break;
        }        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.RotationMode == true)
            {
                Action = ActionPart.Death;
            }
        }

        if(collision.gameObject.tag == "Enemy") {
            speed_x *= -1;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Grounds") {
            speed_x *= -1;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void WaitAction()
    {

    }

    void RaidAction()
    {
        var velocity = rigidbody.velocity;
        velocity.x = speed_x;
        rigidbody.velocity = velocity;
    }

    void DeathAction()
    {
        Destroy(gameObject);
    }

    // カメラの範囲内に入ったときに攻撃パートに切り替える
    private void OnBecameVisible() {
        Action = ActionPart.Raid;
    }
}
