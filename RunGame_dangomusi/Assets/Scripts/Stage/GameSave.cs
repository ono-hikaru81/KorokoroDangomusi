using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    public int stageNum = 0;
    
    // [引数１]ステージ番号 [返り値]0 = 正常完了
    int SaveProcessing(int StageNum) {
        if(StageNum < 1 || StageNum > 3) {
            return 1;
        }
        PlayerPrefs.SetInt("StageNum", StageNum);
        PlayerPrefs.Save();
        return 0;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {

            switch (SaveProcessing(stageNum)) {
                case 0: Debug.Log("セーブ完了");
                    break;
                case 1: Debug.Log("存在しないステージ番号です");
                    break;
            }
        }
    }
}
