using UnityEngine;
using System.Collections;
using System;

public class PaketControl : MonoBehaviour {

    public Vector3 startPosition_;      //発射地点の座標
    public Vector3 landingPosition_;    //着地地点の座標
    public bool shotJudge_ = false;     //trueの時paketを発射する
    public int divNumber_ = 500;        //1フレームに進む分割数
    public int vertex_ = 100;           //パケットの楕円軌道のy座標最高地点  
    public float shotDegree_;           //発射地点からみた着地地点の角度     

    private double distanceX_;
    private double distanceZ_;
    private double distance_;   //発射位置と着地位置の距離

    private float speedX_;
    private float speedZ_;

    // Use this for initialization
    void Start () {
        //Debug.Log("Degree = " + shotDegree_);
        //Debug.Log("Paket Control Start");

        StartCoroutine("Judge");
	}

    //shotJudgeの監視、trueになった時発射命令を出す.
    IEnumerator Judge() {
        while (true) {
            if(shotJudge_ == true) {
                this.transform.position = startPosition_;
                StartCoroutine("DistanceCalculation");
                yield break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    IEnumerator DistanceCalculation() {
        distanceX_ = startPosition_.x - landingPosition_.x;
        distanceZ_ = startPosition_.z - landingPosition_.z;

        if (distanceX_ < 0) {
            distanceX_ = -(distanceX_);
        }

        if (distanceZ_ < 0) {
            distanceZ_ = -(distanceZ_);
        }

        distance_ = Math.Sqrt(Math.Pow(distanceX_, 2) + Math.Pow(distanceZ_, 2));
        //Debug.Log("distance is " + distance_);  //XZ距離の表示

        speedX_ = (float)distanceX_ / divNumber_;    //x移動のスピード調整
        speedZ_ = (float)distanceZ_ / divNumber_;    //z移動のスピード調整

        StartCoroutine("Control");
        
        yield break;
    }

    IEnumerator Control() {
        double moveProgress = -(distance_); //xy座標の進捗

    float x;    //x座標がどのくらい進んだか
    float z;    //z座標がどのくらい進んだか

    float y = 0;    //y座標

        int count = 0;
        while (true) {
            count++;
            moveProgress += distance_ * 2 / divNumber_;
          
            if(startPosition_.x <= landingPosition_.x && startPosition_.z <= landingPosition_.z) {         //右上
                //Debug.Log("1");
                x = (float)(this.transform.position.x + (distanceX_ / divNumber_));
                z = (float)(this.transform.position.z + (distanceZ_ / divNumber_));
            } else if(startPosition_.x >= landingPosition_.x && startPosition_.z <= landingPosition_.z) {  //左上
                //Debug.Log("2");
                x = (float)(this.transform.position.x - (distanceX_ / divNumber_));
                z = (float)(this.transform.position.z + (distanceZ_ / divNumber_));
            } else if(startPosition_.x >= landingPosition_.x && startPosition_.z >= landingPosition_.z) {  //左下
                //Debug.Log("3");
                x = (float)(this.transform.position.x - (distanceX_ / divNumber_));
                z = (float)(this.transform.position.z - (distanceZ_ / divNumber_));
            } else if(startPosition_.x <= landingPosition_.x && startPosition_.z >= landingPosition_.z) {  //右下
                //Debug.Log("4");
                x = (float)(this.transform.position.x + (distanceX_ / divNumber_));
                z = (float)(this.transform.position.z - (distanceZ_ / divNumber_));
            } else { //エラー
                Debug.Log("Direction Error");   //発射地点と着地地点が同じ時
                Destroy(this.gameObject);
                yield break;
            }

            //y座標の計算
            y = (int)(Math.Sqrt(Math.Pow(vertex_, 2) - ((Math.Pow(vertex_, 2) * (Math.Pow(moveProgress, 2))) / Math.Pow(distance_, 2))));

            //全ての試行が終わったところでパケット移動の終了
            if(count >= divNumber_) {
                Destroy(this.gameObject);
                yield break;
            }

            //パケット移動の試行
            this.transform.position = new Vector3(x, y, z);

            yield return new WaitForSeconds(0.001f);
        }
    }
}
