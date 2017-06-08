using System;
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
        left = queList.Count;
        if (left == 0)
            return false;
        setParams();
        return true;
    }

    public void setParams()
    {
        foreach (IDictionary parser in queList[0])
        {
            country = (string) parser["country"];
        }
        queList.RemoveAt(0);
    }
}

