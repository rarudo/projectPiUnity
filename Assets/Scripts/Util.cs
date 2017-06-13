using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour {
	private GameObject America,Europe,China,Russia,Australia,Japan,Brazil,Africa;
	//IPと座標を紐付ける辞書
	private Dictionary<string, Vector2> ipMap = new Dictionary<string, Vector2>();

	void Awake(){
		America = GameObject.Find("CountryObject/America");
		Europe = GameObject.Find("CountryObject/Europe");
		China = GameObject.Find("CountryObject/China");
		Russia = GameObject.Find("CountryObject/Russia");
		Australia = GameObject.Find("CountryObject/Australia");
		Japan = GameObject.Find("CountryObject/Japan");
		Brazil = GameObject.Find("CountryObject/Brazil");
		Africa = GameObject.Find("CountryObject/Africa");
	}

	public Vector2 GetPosition(string country,string ipAddress)
	{
		if (ipMap.ContainsKey (ipAddress)) {
			return ipMap[ipAddress];
		}

		GameObject gobj;
		switch (country){
				case "America":
					gobj = America;
					break;
				case "Europe":
					gobj = Europe;
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
				case "Africa":
					gobj = Africa;
					break;
				case "Brazil":
					gobj = Brazil;
					break;
				default:
					print("国名が不正");
					return new Vector2 (0,0);
					break;
		}



		//幅,高さを取得
		//実世界と10倍の差があるのは謎
		//たぶんどっかの親オブジェクトのスケールが加えられている？
		float width = gobj.transform.lossyScale.x * 10;
		float height = gobj.transform.lossyScale.z * 10;

		//ポジションを取得 center x
		float cx = gobj.transform.position.x;
		float cy = gobj.transform.position.z;

		//長方形の中心からの法線を取得
		float y = (width / 2);
		float x = (height / 2);

		//長方形の中からランダムで一箇所取得する
		int rx = (int)Random.Range(-(width / 2), (width / 2));
		int ry = (int)Random.Range(-(height / 2), (height / 2));

		//長方形に対してアフィン変換をする
		//実世界と90度ずれている。。。謎だね
		float dx =  rx*Mathf.Sin ((gobj.transform.eulerAngles.y+90)* Mathf.Deg2Rad)+ry*Mathf.Cos((gobj.transform.eulerAngles.y+90)* Mathf.Deg2Rad);
		float dy = rx*Mathf.Cos((gobj.transform.eulerAngles.y+90)* Mathf.Deg2Rad)-ry*Mathf.Sin((gobj.transform.eulerAngles.y+90)* Mathf.Deg2Rad);

		ipMap.Add (ipAddress, new Vector2 (cx + dx, cy + dy));
		return new Vector2 (cx+dx,  cy+dy);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
