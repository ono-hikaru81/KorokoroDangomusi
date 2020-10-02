using RunGame.GameClear;
using RunGame.SelectStage;
using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // ←追加

namespace RunGame.Title
{
    /// <summary>
    /// 『タイトル画面』のシーン遷移を制御します。
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 「StartButton」をクリックした際に
        /// 呼び出されます。
        /// </summary>
        public void OnClickStartButton()
        {
            PlayerPrefs.SetInt("isContinue", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Stage 0");
        }

        public void OnClickLoadButton() {

            PlayerPrefs.SetInt("isContinue", 0);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Stage 0");
        }

    }
}