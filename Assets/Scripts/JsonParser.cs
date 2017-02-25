using System.Collections;
using MiniJSON;
using UnityEngine;

public class JsonParser : MonoBehaviour {
    void Start()
    {
        StartCoroutine(Loop());
    }

    IEnumerator Loop(){
        while (true)
        {
            yield return StartCoroutine(setQues());
        }
    }


    IEnumerator setQues()
    {
        //すでにqueが溜まってたらスキップ
        if (shotQue.left > 20000)
            yield break;
        // サーバからJSON文字列取得
        long lastId = shotQue.getLastId();
        WWW www = new WWW ("http://0.0.0.0:3000/getTask?limit=500&start="+lastId);
        yield return www;
        // クラスにデータを取得
        // JSONデータは最初は配列から始まるので、Deserialize（デコード）した直後にリストへキャスト
        IList dataList= (IList)Json.Deserialize(www.text);
        Scanner scanner = GetComponent<Scanner>();
        foreach(IDictionary person in dataList)
        {
            long id = (long)person["id"];
            long port = (long) person["port"];
            string strX= (string)person["x"];
            string strY= (string)person["y"];
            string date= (string)person["date"];
            float x = float.Parse(strX);
            float y = float.Parse(strY);
            shotQue.AddQue(id,x,y,date,port);
        }
    }
}
