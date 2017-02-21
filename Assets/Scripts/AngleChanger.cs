using UnityEngine;
using System.Collections;

public class AngleChanger : MonoBehaviour {

    public Camera mainCamera_;  //メインカメラ
    public Camera subCamera1_;  //サブカメラ1

    public int cNum;    //現在のカメラ

    void Start() {
        cNum = 0;   //カメラの初期化
        subCamera1_.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        /*
         * キーボードでのカメラ切り替えはテスト用
         */
        if (Input.GetKey("a")) {
            mainCamera_.enabled = true;
            subCamera1_.enabled = false;
        } else if (Input.GetKey("s")) {
            subCamera1_.enabled = true;
            mainCamera_.enabled = false;
        }
    }

    public void OnClick() {
        switch (cNum) {
            case 0:
                mainCamera_.enabled = false;
                subCamera1_.enabled = true;
                Debug.Log("サブカメラ1有効化");
                cNum = 1;   //subカメラ1に切り替えたことを知らせる
                break;
            case 1:
                subCamera1_.enabled = false;
                mainCamera_.enabled = true;
                Debug.Log("メインカメラ有効化");
                cNum = 0;   //mainカメラに切り替えたことを知らせる
                break;
            default:
                Debug.Log("[ERROR] cNum is unknown");
                break;
        }
    }
}