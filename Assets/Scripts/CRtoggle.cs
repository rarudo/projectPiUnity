using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CRtoggle : MonoBehaviour {

    public GameObject CameraRotateController_;
    public Toggle toggle_;
    
    // Use this for initialization
    void Start () {
        toggle_ = GetComponent<Toggle>();
        StartCoroutine("Control");
	}
	
    IEnumerator Control() {
        JudgeFalse:
        while (true) {
            if (toggle_.isOn == false) {
                CameraRotateController_.GetComponent<CameraRotation>().rotateJudge_ = false;
                goto JudgeTrue;     //trueの判定へ
            }
            yield return new WaitForSeconds(0.001f);
        }

        JudgeTrue:
        while (true) {
            if (toggle_.isOn == true) {
                CameraRotateController_.GetComponent<CameraRotation>().rotateJudge_ = true;
                goto JudgeFalse;    //falseの判定へ
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
}
