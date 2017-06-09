using UnityEngine;
using System.Collections;

// ファイル読み込み
public class Scanner : MonoBehaviour {

    public GameObject TcpPaket;
    public GameObject UdpPaket;

    public GameObject PositionControl;

    //for Debug
    public Vector2 TestSt;
    public Vector2 TestLa;
    public int TestType;
    public Vector2 ShowSt;
    public Vector2 ShowLa;

    //for Debug
    public void OnClick() {
        Shot(TestSt, TestLa, TestType);
    }

    void Update() {
        //Shot(TestSt, TestLa, TestType);
    }

    public void Shot(Vector2 start)
    {
        Shot(start,TestLa,0);
    }

    //start:発射座標 landding:着地座標 type:TCPなら0、UDPなら1
    public void Shot(Vector2 start, Vector2 landding, int type) {

        if(start.x < -29) {
            start.x = 180 + (180 -(-(start.x)));
        }
        if (landding.x < -29) {
            landding.x = 180 + (180 -(-(landding.x)));
        }

        //10進法変換用
        Vector2 st10;
        Vector2 lan10;

        // st10.x = start.x * 16.666666666666f + 500;
        // st10.y = start.y * 16.666666666666f + 1500;
        st10.x = start.x;
        st10.y = start.y;
        lan10.x = landding.x * 16.666666666666f + 500;
        lan10.y = landding.y * 16.666666666666f + 1500;

        //for Debug
        ShowSt.x = st10.x;
        ShowSt.y = st10.y;
        ShowLa.x = lan10.x;
        ShowLa.y = lan10.y;

        if (type == 0) {
            Tcp((int)st10.x, (int)st10.y, (int)lan10.x, (int)lan10.y);
        }else if(type == 1) {
            Udp((int)st10.x, (int)st10.y, (int)lan10.x, (int)lan10.y);
        }
    }

    void Tcp(int stX, int stY, int lanX, int lanY) {
        PaketControl paketControl_;
        //Debug.Log("tcp");
        GameObject tcpPaket = GameObject.Instantiate(TcpPaket);
        tcpPaket.transform.parent = PositionControl.transform;
        tcpPaket.name = "T:" + stX + ":" + stY + " to " + lanX + ":" + lanY ;

        paketControl_ = tcpPaket.GetComponent<PaketControl>();
        paketControl_.startPosition_.x = stX;
        paketControl_.startPosition_.z = stY;
        paketControl_.landingPosition_.x = lanX;
        paketControl_.landingPosition_.z = lanY;
        paketControl_.shotJudge_ = true;    //発射
    }

    void Udp(int stX, int stY, int lanX, int lanY) {
        PaketControl paketControl_;
        //Debug.Log("udp");
        GameObject tcpPaket = GameObject.Instantiate(UdpPaket);
        tcpPaket.transform.parent = PositionControl.transform;
        tcpPaket.name = "U:" + stX + ":" + stY + " to " + lanX + ":" + lanY ;

        paketControl_ = tcpPaket.GetComponent<PaketControl>();
        paketControl_.startPosition_.x = stX;
        paketControl_.startPosition_.z = stY;
        paketControl_.landingPosition_.x = lanX;
        paketControl_.landingPosition_.z = lanY;
        paketControl_.shotJudge_ = true;    //発射
    }
}
