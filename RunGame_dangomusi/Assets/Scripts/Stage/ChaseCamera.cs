using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame.Stage
{
    /// <summary>
    /// 追尾カメラを表します。
    /// </summary>
    public class ChaseCamera : MonoBehaviour
    {
        // 表示範囲のずらす量
        public float cameraOffset_X = 0;
        public float cameraOffset_Y = 0;

        // 追尾対象(プレイヤー)
        Transform target;

        // Start is called before the first frame update
        void Start() {
            // 他のゲームオブジェクトを参照
            target = GameObject.FindGameObjectWithTag("Player").transform;

            // 追尾対象が未指定の場合は"Player"タグのオブジェクトで設定
            if (target == null) {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        // Update is called once per frame
        void Update() {
            if (target != null) {
                var position = Camera.main.transform.position;
                position.x = target.position.x + cameraOffset_X;
                position.y = target.position.y + cameraOffset_Y;
                //position.z = target.position.z;
                Camera.main.transform.position = position;
            }
        }
    }
}