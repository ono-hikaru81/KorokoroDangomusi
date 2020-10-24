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
        /// <summary>
        /// 「StageButton」の親オブジェクトを指定します。
        /// </summary>
        [SerializeField]
        private Transform[] buttons = null;

        /// <summary>
        /// ボタン選択時の表示スケールを指定します。
        /// </summary>
        [SerializeField]
        private Vector3 selectedScale = new Vector3(1.1f, 1.1f, 1);

        // 現在選択されているボタンを示すインデックス
        int selectedIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            // GameControllerからステージ名一覧を取得
            var stageNames = GameController.Instance.StageNames;
        }

        // Update is called once per frame
        void Update()
        {
            // 上カーソルキーが押された場合
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                selectedIndex--;
            }
            // 下カーソルキーが押された場合
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                selectedIndex++;
            }
            // 「Enter」キーが押された場合
            else if (Input.GetKeyUp(KeyCode.Return))
            {
                // 「NEW GAME」選択中
                if (selectedIndex == 0)
                {
                    // 現在起動しているシーンを再読み込み
                    PlayerPrefs.SetInt("isContinue", 0);
                    PlayerPrefs.SetFloat("PlayerPosX", 0.0f);
                    PlayerPrefs.SetFloat("PlayerPosY", 3.0f);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("OpeningMovie");
                }
                // 「CONTINUE」選択中
                else if (selectedIndex == 1)
                {
                    PlayerPrefs.SetInt("isContinue", 1);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("Stage 0");
                }
            }

            // 0～ボタン配列の最大数から飛び出ないように
            selectedIndex = Mathf.Clamp(selectedIndex, 0, buttons.Length - 1);
            // ボタン配列のすべての要素を繰り返し処理
            for (int index = 0; index < buttons.Length; index++)
            {
                // 選択中のボタンのみ拡大
                if (index == selectedIndex)
                {
                    buttons[index].localScale = new Vector3(1.3f, 1.3f, 1);
                }
                else
                {
                    buttons[index].localScale = Vector3.one;
                }
            }
        }

        /// <summary>
        /// 「StartButton」をクリックした際に
        /// 呼び出されます。
        /// </summary>
        public void OnClickStartButton()
        {
            PlayerPrefs.SetInt("isContinue", 0);
            PlayerPrefs.SetFloat("PlayerPosX", 0.0f);
            PlayerPrefs.SetFloat("PlayerPosY", 3.0f);
            PlayerPrefs.Save();
            SceneManager.LoadScene("OpeningMovie");
        }

        public void OnClickLoadButton() {

            PlayerPrefs.SetInt("isContinue", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Stage 0");
        }
    }
}