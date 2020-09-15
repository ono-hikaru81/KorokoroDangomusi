using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// <summary>
/// 敵の『ケムシ』を表します。
/// </summary>
public class Caterpiller : MonoBehaviour
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

    // ケムシの速度
    float speed_x = 0.0f;
    float speed_y = 0.0f;
    float speed_z = 0.0f;

    // 時間管理変数
    float motiontimer = 0.0f;
    float pausetimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    void WaitAction()
    {

    }

    void RaidAction()
    {

    }

    void DeathAction()
    {
        Destroy(gameObject);
    }
}
