using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class ShotController : MonoBehaviour
{
    public int speed;
    private Scanner scanner;
    private Text datText;
    private Slider shotSpeedSlider;
    private PortCubeController _portCubeController;

	// Use this for initialization
	void Start ()
	{
        scanner = GetComponent<Scanner>();
        GameObject dataObj,sliderObj;

	    dataObj = GameObject.Find("DateText");
	    sliderObj = GameObject.Find("ShotSpeedSlider");

	    _portCubeController = GameObject.Find("PortCubes").GetComponent<PortCubeController>();
	    datText = dataObj.GetComponent<Text>();
	    shotSpeedSlider = sliderObj.GetComponent<Slider>();
	    speed = (int) shotSpeedSlider.value;


	    StartCoroutine(Loop());
	}

    private IEnumerator Loop()
    {
        while (true)
        {
            for(int i = 0; i < speed; i++)
                StartCoroutine(StartShot());
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator StartShot()
    {
	    Vector2 vec2;
	    while (shotQue.fechAll())
	    {
	        vec2.x = shotQue.x;
	        vec2.y = shotQue.y;
	        scanner.Shot(vec2);
	        _portCubeController.Emmission(shotQue.port);
	        datText.text = shotQue.date;
	        yield break;
	    }
    }
	
	// Update is called once per frame
	void Update ()
	{
	    speed = (int) shotSpeedSlider.value;
	}
}
