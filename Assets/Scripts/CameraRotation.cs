using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

    public bool rotateJudge_;
    public int rotateSpeed_;

    public void Start() {
        StartCoroutine("Judge");
        StartCoroutine("RotateControl");
    }

    public void Update() {
        if(rotateJudge_ == false) {
            StopCoroutine("RotateControl");
        }
    }

    IEnumerator Judge() {
        JudgeFalse:
        while (true) {
            if (rotateJudge_ == false) {
                StopCoroutine("RotateControl");
                goto JudgeTrue;     //trueの判定へ
            }
            yield return new WaitForSeconds(0.001f);
        }

        JudgeTrue:
        while(true) {
            if(rotateJudge_ == true) {
                StartCoroutine("RotateControl");
                goto JudgeFalse;    //falseの判定へ
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    IEnumerator RotateControl() {
        while (true) {
            this.transform.Rotate(new Vector3(0, rotateSpeed_, 0) * Time.deltaTime);
            yield return new WaitForSeconds(0.001f);
        }
    }
}
