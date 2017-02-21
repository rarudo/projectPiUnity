using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PaketCounter : MonoBehaviour {

    public GameObject positionControl_; //Packetオブジェクトの親オブジェクト
    public Text paketCounter_;

    private int paketCount_;    //現在のPacketの表示数

	// Use this for initialization
	void Start () {
        //Debug.Log("PaketCounter is Loaded");
        paketCount_ = 0;    //カウントの初期化
    }
	
	// Update is called once per frame
	void Update () {
        paketCount_ = positionControl_.transform.childCount;
        paketCounter_.text = "PaketCounet: " + paketCount_;
	}
}
