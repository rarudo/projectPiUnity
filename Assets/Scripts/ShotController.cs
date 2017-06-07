using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class ShotController : MonoBehaviour
{
    public int speed = 20;
    private Scanner scanner;
    private Text datText;
    private Slider shotSpeedSlider;
	private ShotQue _shotQue;

	// Use this for initialization
	void Start ()
	{
        scanner = GetComponent<Scanner>();
        GameObject dataObj,sliderObj;

        _shotQue = GameObject.Find("Systems").GetComponent<ShotQue>();
	    dataObj = GameObject.Find("DateText");
	    sliderObj = GameObject.Find("ShotSpeedSlider");

	    //_portCubeController = GameObject.Find("PortCubes").GetComponent<PortCubeController>();
	    datText = dataObj.GetComponent<Text>();
	    shotSpeedSlider = sliderObj.GetComponent<Slider>();
	    speed = (int) shotSpeedSlider.value;


	    StartCoroutine(Loop());
	}

    private IEnumerator Loop()
    {
        while (true)
        {
            StartCoroutine(StartShot());
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator StartShot()
    {
	    Vector2 vec2;
	    print(_shotQue.left);
	    while (_shotQue.fechAll())
	    {
	        vec2.x = _shotQue.x;
	        vec2.y = _shotQue.y;
	        scanner.Shot(vec2);
	       // _portCubeController.Emmission(_shotQue.port);
	        datText.text = _shotQue.date;
	        yield break;
	    }
    }
	
	// Update is called once per frame
	void Update ()
	{
	   // speed = (int) shotSpeedSlider.value;
	}
}
