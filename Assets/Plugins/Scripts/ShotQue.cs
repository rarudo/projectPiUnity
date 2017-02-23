using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public static class shotQue
{
    //残りいくつ残ってるか
    public static int left;

    public static long id;
    public static float x;
    public static float y;
    public static string date;

    private static List<Dictionary<string,object>> queList;

    static shotQue(){
        //初期化
        left = 0;
        queList = new List<Dictionary<string, object>>();
    }

    public static void AddQue(long id, float x, float y, string date)
    {
        Dictionary<string,object> dict = new Dictionary<string, object>()
        {
            {"id", id},
            {"x", x},
            {"y", y},
            {"date", date}
        };
        queList.Add(dict);
        setLeft();
    }

    private static void setLeft()
    {
        left = queList.Count;
    }

    private static void setParams()
    {
        //最初の要素を取得
        Dictionary<string,object> dict = queList[0];
        queList.RemoveAt(0);
        setLeft();
        x = (float) dict["x"];
        y = (float) dict["y"];
        id = (long) dict["id"];
        date = (string) dict["date"];
    }

    public static bool fechAll()
    {
        if (left == 0)
            return false;
        setParams();
        return true;
    }

    public static long getLastId()
    {
        try
        {
            //最後のdictを取得
            Dictionary<string,object> dict = queList[queList.Count-1];
            return (long) dict["id"];
        }
        catch
        {
            //listに何も入ってない時
            return 1;
        }
    }
}

