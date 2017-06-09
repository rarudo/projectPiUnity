using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class ShotController : MonoBehaviour
{
    private Scanner scanner;
    private Text datText;
    private Slider shotSpeedSlider;
	private ShotQue shotQue;

	// Use this for initialization
	void Start ()
	{
        scanner = GetComponent<Scanner>();
        GameObject dataObj,sliderObj;
        shotQue = GameObject.Find("Systems").GetComponent<ShotQue>();
	    dataObj = GameObject.Find("DateText");
	    sliderObj = GameObject.Find("ShotSpeedSlider");
	    datText = dataObj.GetComponent<Text>();
	    shotSpeedSlider = sliderObj.GetComponent<Slider>();
	    StartCoroutine(Loop());
	}

    private IEnumerator Loop()
    {
        while (true)
        {
            StartCoroutine(StartShot());
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator StartShot()
    {
	    Vector2 vec2;
	    List<Vector2> que = shotQue.getQue();
	    foreach (var pos in que)
	    {
	        vec2.x = pos.x;
	        vec2.y = pos.y;
	        scanner.Shot(vec2);
	    }
	    yield break;
    }
}
