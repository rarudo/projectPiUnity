using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour {
	private GameObject America,England,China,Russia,Australia,Japan;

	// Use this for initialization
	void Start () {
		America = GameObject.Find("America");
		England = GameObject.Find("England");
		China = GameObject.Find("China");
		Russia = GameObject.Find("Russia");
		Australia = GameObject.Find("Australia");
		Japan = GameObject.Find("Japan");
	}

	public Vector2 GetPosition(string country)
	{
		GameObject gobj = Japan;
		switch (country)
		{
				case "America":
					gobj = America;
					break;
				case "England":
					gobj = England;
					break;
				case "China":
					gobj = China;
					break;
				case "Russia":
					gobj = Russia;
					break;
				case "Australlia":
					gobj = Australia;
					break;
				case "Japan":
					gobj = Japan;
					break;
		}
		float width = gobj.transform.localScale.x;
		float height = gobj.transform.localScale.z; 
		//範囲用オブジェクトのxy座標を取得
		//transform.positionで取れる値はオブジェクトの中心なので
		//オブジェクトの左下の座標をobjX objYに設定する
		float objX = gobj.transform.position.x - width/2;
		float objY = gobj.transform.position.z - height/2;
		//範囲の中からランダムな座標を取得する
		int x = (int)Random.Range(objX, objX + width);
		int y = (int)Random.Range(objY, objY + height);
		return new Vector2(x,y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
