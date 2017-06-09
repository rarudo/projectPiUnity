using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class ShotController : MonoBehaviour
{
    public int speed = 2;
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
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator StartShot()
    {
	    Vector2 vec2;
	    List<Vector2> que = _shotQue.getQue();
	    foreach (var pos in que)
	    {
	        vec2.x = pos.x;
	        vec2.y = pos.y;
	        scanner.Shot(vec2);
	    }
	    yield break;
    }
}
