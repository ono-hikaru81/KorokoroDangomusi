using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// 敵の『アリ』を表します。
/// </summary>
public class Ants : MonoBehaviour
{
    public enum ActionPart
    {
        Wait, // 待機モーション
        Raid, // 戦闘モーション
        Death // 死亡モーション
    }
    ActionPart Action = ActionPart.Wait;

    // アリの速度
    float speed_x = 0.005f;
    float speed_y = 0.0f;
    float speed_z = 0.0f;

    // 
    float motiontimer = 0.0f;
    float pausetimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
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

    void WaitAction()
    {
        transform.Translate(speed_x, speed_y, speed_z);

        motiontimer += Time.deltaTime;
        if (motiontimer >= 2.0f)
        {
            pausetimer += Time.deltaTime;
            speed_x *= -1;
            motiontimer = 0;
        }
    }

    void RaidAction()
    {

    }

    void DeathAction()
    {

    }
}
