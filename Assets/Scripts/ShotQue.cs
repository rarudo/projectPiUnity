﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class ShotQue : MonoBehaviour
{
    //残りいくつ残ってるか
    public int left;

    public long id;
    public String country;
    public string date;
    public long port;
    public float x, y;

    public List<IList> queList;

    void Start(){
        //初期化
        left = 0;
        queList = new List<IList>();
    }

    public void AddQue(IList parsedJson)
    {
        queList.Add(parsedJson);
    }


    public bool fechAll()
    {
        
        // left = queList.Count;
        //デバッグ用
        left = 1;
        if (left == 0)
            return false;
        setParams();
        return true;
    }

    public void setParams()
    {
        //　デバッグ用
        x = 0;
        y = 0;
        
       // foreach (IDictionary parser in queList[0])
       // {
       //     //受け取ったjsonから国を抽出する
       //     country = (string) parser["country"];
       //     /*
       //     Vector2 xy = getPosition("America");
       //     
       //     ここにxとy代入しとけば、勝手に発射される
       //     x = xy.x
       //     y = xy.y
       //     */
       // }
       // queList.RemoveAt(0);
    }
}

