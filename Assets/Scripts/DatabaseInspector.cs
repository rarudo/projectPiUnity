using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MiniJSON;

public class DatabaseInspector : MonoBehaviour
{
	public float timeOut = 1f;

	private List<float> data_ = new List<float>();
	private List<float> ip_infomation_ = new List<float>();

	private float old_data;
	private float old_ip_infomation;
	void Start() {
		StartCoroutine(initVar());
	}
	IEnumerator initVar(){
		WWW www = new WWW("http://0.0.0.0:3000/getStatus");
		yield return www;
		Dictionary<string, object> dic = Json.Deserialize(www.text) as Dictionary<string, object>;
		{
			int c_data = int.Parse (dic ["data"].ToString ());
			int c_ip_infomation = int.Parse (dic ["ip_infomation"].ToString ());
			print ("初期値: data:"+c_data);
			print ("初期値: infomation:"+c_ip_infomation);
			old_data = c_data - 1;
			old_ip_infomation = c_ip_infomation - 1;
		}

		StartCoroutine( FuncCoroutine() );
	}

	IEnumerator FuncCoroutine() {
		while(true){
			WWW www = new WWW("http://0.0.0.0:3000/getStatus");
			yield return www;
			Dictionary<string, object> dic = Json.Deserialize(www.text) as Dictionary<string, object>;
			{
				int c_data = int.Parse (dic ["data"].ToString ());
				int c_ip_infomation = int.Parse (dic ["ip_infomation"].ToString ());
				print (c_data - old_data);
				print (c_ip_infomation - old_ip_infomation);
				data_.Add(c_data - old_data);
				ip_infomation_.Add(c_ip_infomation - old_ip_infomation);

				//前の値の更新
				old_data = c_data;
				old_ip_infomation = c_ip_infomation;
			}

			if (data_.Count > 4) {
				data_.RemoveAt(0);
			}

			yield return new WaitForSeconds(timeOut);
		}
	}

	void Update() 
	{
	}

	void OnGUI()
	{
		var area = GUILayoutUtility.GetRect(Screen.width/4, Screen.height/4);
		area.y += Screen.height / 4;

		// Grid
		const int div = 10;
		for (int i = 0; i <= div; ++i) {
			var lineColor = (i == 0 || i == div) ? Color.white : Color.gray;
			var lineWidth = (i == 0 || i == div) ? 2f : 1f;
			var x = (area.width  / div) * i;
			var y = (area.height / div) * i;
			Drawing.DrawLine (
				new Vector2(area.x + x, area.y),
				new Vector2(area.x + x, area.yMax), lineColor, lineWidth, true);
			Drawing.DrawLine (
				new Vector2(area.x,    area.y + y),
				new Vector2(area.xMax, area.y + y), lineColor, lineWidth, true);
		}

		//グラフの上限値を求める
		float max = 1;
		if (data_.Count > 0) {
			max = data_.Max ();
		}
		if (ip_infomation_.Count > 0) {
			if(max < ip_infomation_.Max())
				max = ip_infomation_.Max();
		}
		max *= 1.1f;

		// Data
		if (data_.Count > 0) {
			var dx  = area.width / data_.Count; 
			var dy  = area.height / max;
			Vector2 previousPos = new Vector2(area.x, area.yMax); 
			for (var i = 0; i < data_.Count; ++i) {
				var x = area.x + dx * i;
				var y = area.yMax - dy * data_[i];
				var currentPos = new Vector2(x, y);
//				Drawing.DrawLine(previousPos, currentPos, Color.red, 2f, true);
				previousPos = currentPos;
			}
		}

		// ip_infomation
		if (ip_infomation_.Count > 0) {
			var dx  = area.width / ip_infomation_.Count; 
			var dy  = area.height / max;
			Vector2 previousPos = new Vector2(area.x, area.yMax); 
			for (var i = 0; i < ip_infomation_.Count; ++i) {
				var x = area.x + dx * i;
				var y = area.yMax - dy * ip_infomation_[i];
				var currentPos = new Vector2(x, y);
				Drawing.DrawLine(previousPos, currentPos, Color.green, 1f, true);
				previousPos = currentPos;
			}
		}
	}
}