using UnityEngine;
using System.Collections;

public class PaketTestButton : MonoBehaviour {

    public GameObject tcpPaket_;
    public GameObject udpPaket_;

    public Vector2 start_;
    public Vector2 landding_;

    private PaketControl paketControl_;

    public GameObject PositionControl_;

    public void Start() {
        StartCoroutine("PaketTest");
        StartCoroutine("PaketTest2");
    }

    public void Update() {
        start_.x = Random.Range(0, 6001);
        start_.y = Random.Range(0, 3001);
        landding_.x = Random.Range(0, 6001);
        landding_.y = Random.Range(0, 3001);
        //Debug.Log(start_ + " " + landding_);
    }

    /// ボタンをクリックした時の処理
    public void OnClick() {
        //Debug.Log("Button click!");

        GameObject tcpPaket = GameObject.Instantiate(tcpPaket_) as GameObject;
        tcpPaket.transform.parent = PositionControl_.transform;
        tcpPaket.name = "TestPaket";

        paketControl_ = tcpPaket.GetComponent<PaketControl>();
        //paketControl_.startPosition_ = new Vector3(960f, 0f, 570f);
        paketControl_.startPosition_.x = start_.x;
        paketControl_.startPosition_.z = start_.y;
        paketControl_.landingPosition_.x = landding_.x;
        paketControl_.landingPosition_.z = landding_.y;
        paketControl_.shotJudge_ = true;    //発射
    }

    IEnumerator PaketTest() {
        while (true) {
            GameObject tcpPaket = GameObject.Instantiate(tcpPaket_) as GameObject;
            tcpPaket.transform.parent = PositionControl_.transform;
            tcpPaket.name = "TCPpacket";

            paketControl_ = tcpPaket.GetComponent<PaketControl>();
            //paketControl_.startPosition_ = new Vector3(960f, 0f, 570f);
            paketControl_.startPosition_.x = start_.x;
            paketControl_.startPosition_.z = start_.y;
            paketControl_.landingPosition_.x = landding_.x;
            paketControl_.landingPosition_.z = landding_.y;
            //Debug.Log("Packet : " + start_ + " to " + landding_);
            paketControl_.shotJudge_ = true;    //発射

            yield return new WaitForSeconds(0.0001f);
        }
    }

    IEnumerator PaketTest2() {
        while (true) {
            GameObject udpPaket = GameObject.Instantiate(udpPaket_) as GameObject;
            udpPaket.transform.parent = PositionControl_.transform;
            udpPaket.name = "UDPpacket";

            paketControl_ = udpPaket.GetComponent<PaketControl>();
            //paketControl_.startPosition_ = new Vector3(960f, 0f, 570f);
            paketControl_.startPosition_.x = start_.x;
            paketControl_.startPosition_.z = start_.y;
            paketControl_.landingPosition_.x = landding_.x;
            paketControl_.landingPosition_.z = landding_.y;
            //Debug.Log("Packet : " + start_ + " to " + landding_);
            paketControl_.shotJudge_ = true;    //発射

            yield return new WaitForSeconds(0.0001f);
        }
    }
}