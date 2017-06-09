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
        if (_shotQue.left > 20000)
            yield break;
        // サーバからJSON文字列取得
        WWW www = new WWW ("http://172.16.119.1:5000/getTask");
        yield return www;
        // クラスにデータを取得
        // JSONデータは最初は配列から始まるので、Deserialize（デコード）した直後にリストへキャスト
        if(www.text.Length != 0)
        {
            IList dataList= (IList)Json.Deserialize(www.text);
            _shotQue.addJson(dataList);
        }
    }
}
