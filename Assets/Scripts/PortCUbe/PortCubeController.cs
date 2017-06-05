﻿using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortCubeController : MonoBehaviour
{
    private GameObject pCubePrefab;
    private GameObject[,,] pCubObjArray;
    public int xMax;
    public int yMax;
    public int zMax;
    private int testNum;
    public float posDiffX;
    public float posDiffY;
    public float posDiffZ;
   // private int tesNum;

	// Use this for initialization
	void Start ()
	{
	//    tesNum = 1;
	    xMax = 10;
	    yMax = 10;
	    zMax = 10;
	    posDiffX = 400;
	    posDiffY = 160;
	    posDiffZ = 200;
	    pCubObjArray = new GameObject[xMax +1 ,yMax + 1,zMax + 1];
	    pCubePrefab = Resources.Load("Prefabs/PortCube") as GameObject;
	    CreatePortCubes();

	}
	
	// Update is called once per frame
	void Update ()
	{
        //Emmission(tesNum);
	    //tesNum++;
	}

    void CreatePortCubes()
    {
        float startPosX = 400;
        float startPosY = 0;
        float startPosZ = 400;
        Vector3 pos;
        for (int z = 1; z <= zMax; z++)
        {
            for (int y = 1; y <= yMax; y++)
            {
                for(int x= 1; x <= xMax; x++)
                {
                    float zPos = startPosZ + posDiffZ * z;
                    float xPos = startPosX + posDiffX * x;
                    float yPos = startPosY + posDiffY * y;
                    pos = transform.TransformPoint(new Vector3(xPos,yPos,zPos));
                    pCubObjArray[x,y,z] = Instantiate(pCubePrefab, pos, Quaternion.identity);
                    pCubObjArray[x,y,z].name = "x" + x + "y" + y + "z" + z;
                    pCubObjArray[x, y, z].transform.parent = transform;
                }
            }
        }
    }

    //引数は全て1以上の数字
    public GameObject GetObjct(int x, int y, int z)
    {
        return pCubObjArray[x, y, z];
    }

    public void Emmission(int x, int y, int z)
    {
        StartCoroutine(BlinkerCoroutine(x, y, z));
    }

    //1000番目とか数値で与えられた時のx,y,zを計算して発行する
    public void Emmission(long num)
    {
        if(num > xMax*yMax*zMax)
            return;

        int z = Mathf.CeilToInt((float) num / (xMax * zMax));

        //xとyは思いつかなかったから総当たり
        int x = 1;
        int y = 1;
        for (int i = xMax*yMax*(z-1); i < xMax * yMax * zMax; i++)
        {
            //探索終了
            if (i == num)
            {
                //breakってそこで終了しないので？
                //x--して、最後のx++を打ち消す
                x--;
                break;
            }
            //繰り上げ
            if (x > xMax)
            {
                x = 1;
                y++;
            }
            x++;
        }
        Emmission(x,y,z);
    }


    IEnumerator BlinkerCoroutine(int x,int y,int z)
    {
        var originalShader = pCubObjArray[x, y, z].GetComponent<Renderer>().material.shader;
        pCubObjArray[x,y,z].GetComponent<Renderer>().material.shader = Shader.Find( "Standard" );
        Renderer _renderer;
        _renderer = pCubObjArray[x, y, z].GetComponent<Renderer>();
        var originalMaterial = new Material(_renderer.material);
        _renderer.material.SetColor("_EmissionColor", new Color(1, 1, 1));
        yield return new WaitForSeconds(0.5f); //1秒待って
        _renderer.material = originalMaterial; //元に戻す
        pCubObjArray[x, y, z].GetComponent<Renderer>().material.shader = originalShader;
    }
}
