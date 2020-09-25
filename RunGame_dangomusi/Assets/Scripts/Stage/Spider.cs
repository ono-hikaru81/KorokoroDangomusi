using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

// <summary>
/// 敵の『クモ』を表します。
/// </summary>
public class Spider : MonoBehaviour
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

    public enum LookingDirection {
        Left,
        Right
    }
    LookingDirection LDirection = LookingDirection.Left;

    // クモの速度
    float speed_x = 0.0f;
    float speed_y = 0.0f;
    float speed_z = 0.0f;

    // 時間管理変数
    float motiontimer = 3.0f;
    float pausetimer = 0.0f;

    Vector2 atackPos;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        atackPos = transform.position;
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
    }

    void WaitAction()
    {

    }

    void RaidAction()
    {
        if (player != null) {
            TurnOver();
        }

        motiontimer += Time.deltaTime;

        if (motiontimer >= 6.0f) {
            GameObject Poison = (GameObject)Resources.Load("Prefabs/PoisonBall");
            if (LDirection == LookingDirection.Left) {
                atackPos.x -= 1.5f;
                if (Poison != null) {
                    Instantiate(Poison, atackPos, Quaternion.identity);
                }
            }
            else if (LDirection == LookingDirection.Right) {
                atackPos.x += 1.5f;
                if (Poison != null) {
                    Instantiate(Poison, atackPos, Quaternion.identity);
                }
            }
            atackPos.x = transform.position.x;
            motiontimer = pausetimer;
        }
    }

    void DeathAction()
    {
        Destroy(gameObject);
    }

    // カメラの範囲内に入ったときに攻撃パートに切り替える
    private void OnBecameVisible() {
        Action = ActionPart.Raid;
    }

    void TurnOver() {
        if (player != null) {
            var scale = transform.localScale;
            if (player.transform.position.x < transform.position.x) {
                LDirection = LookingDirection.Left;
                scale.x = 0.39f;
            }
            else if (player.transform.position.x > transform.position.x) {
                LDirection = LookingDirection.Right;
                scale.x = -0.39f;
            }
            transform.localScale = scale;
        }
    }
}
