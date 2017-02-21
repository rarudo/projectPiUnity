using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CSslider : MonoBehaviour {

    public GameObject cameraRotateController_;
    public Slider slider_;

    // Use this for initialization
	void Start () {
        slider_.value = 10;
	}
	
	// Update is called once per frame
	void Update () {
        cameraRotateController_.GetComponent<CameraRotation>().rotateSpeed_ = (int)slider_.value;
	}
}
