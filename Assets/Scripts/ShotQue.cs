using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShotQue : MonoBehaviour
{
    //残りいくつ残ってるか
    public int left;

    public long id;
    public String country,ip;
    public string date;
    public long port;
    public float x, y;
    
    //これが核となる変数
    //que.x que.yに発車する座標が入ってる
    public List<Vector2> que;

    //1カプセルの中に、50個のqueが入ってる
    public List<IList> capsuleQue;

    private Util util;

    void Start(){
        util = GameObject.Find("Systems").GetComponent<Util>();
        //初期化
        left = 0;
        capsuleQue = new List<IList>();
        StartCoroutine("setQue");
    }

    public void addJson(IList parsedJson)
    {
        capsuleQue.Add(parsedJson);
    }

    public List<Vector2> getQue()
    {
        int i = que.Count;
        //50個ずつ取得する
        if (i > 50)
        {
            i = 50;
        }
        //queからi個取得する
        List<Vector2> returnQue = que.GetRange(0, i);
        //取り出した後queをi個削除する
        que.RemoveRange(0,i);
        return returnQue;
    }

    //capsuleQueから取り出して、queに格納するメソッド
    public IEnumerator setQue()
    {
        while (true)
        {
            //1フレームごとにループする
            yield return new WaitForEndOfFrame();
            if (capsuleQue.Count != 0)
            {
                foreach (IDictionary parser in capsuleQue[0])
                {
                    //受け取ったjsonから国を抽出する
                    country = (string) parser["country"];
                    //薩摩へこれを基準に判定してください！！！
                    ip = (string) parser["ip"];
                    //country = "Russia";
                    Vector2 xy = util.GetPosition(country);
                    que.Add(xy);
                }
                capsuleQue.RemoveAt(0);
            }
        }
    }
}

