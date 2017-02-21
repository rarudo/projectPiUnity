using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RotationSpeedDisp : MonoBehaviour {

    public GameObject cameraRotateController_;
    public Text speedDisp_;

    private int speed_; //カメラの回転速度

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        speedDisp_.text = "RotationSpeed: " + (cameraRotateController_.GetComponent<CameraRotation>().rotateSpeed_).ToString();
	}
}
