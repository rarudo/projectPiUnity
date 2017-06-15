using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;

public class JsonGetter : MonoBehaviour
{
    private ShotQue _shotQue;
    void Start()
    {
        _shotQue = GameObject.Find("Systems").GetComponent<ShotQue>();
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
        if (_shotQue.left > 20000){
            print("queが20000以上");
            yield break;
        }
        // サーバからJSON文字列取得
        WWW www = new WWW ("http://192.168.98.28:5000/getTask");
        yield return www;
        // クラスにデータを取得
        // JSONデータは最初は配列から始まるので、Deserialize（デコード）した直後にリストへキャスト
        if(www.text.Length != 0)
        {
            IList dataList= (IList)Json.Deserialize(www.text);
            _shotQue.addJson(dataList);
        }
        
        yield return new WaitForSeconds(0.5f);
        ////以下デバッグ用！
        //int rand = Random.Range(1, 20);
        ////テスト用のjsonを作成
        ////  string json = "{\"country\":\"America\",\"ip\":\"192.168.10." + rand + "\"}";
        //string json = "[{\"country\":\"Russia\",\"ip\":\"192.168.10."+rand+"\"}]";
        ////print(json);
        //IList dataList= (IList)Json.Deserialize(json);
        //_shotQue.addJson(dataList);
        ////終了処理
        //yield break;
    }
}
