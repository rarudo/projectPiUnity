using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fps : MonoBehaviour {

    public Text fps_;

    private int x_;    //現在のPacketの表示数

    // Use this for initialization
    void Start() {
        x_ = 0;    //カウントの初期化
    }

    // Update is called once per frame
    void Update() {
        x_ = (int)(1f / Time.deltaTime);
        fps_.text = "FPS: " + x_;
    }
}