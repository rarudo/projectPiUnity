using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

namespace Assets.Scripts
{
    public class MysqlBase : MonoBehaviour{
        private string SERVER = "tk2-218-18711.vs.sakura.ne.jp";
        private string DATABASE = "clusterpi";
        private string USERID = "raspberry";
        private string PORT = "3306";
        private string PASSWORD = "raspberry";
        private string TABLENAME = "ip_infomation,location_route";

        //sql関係の処理が終了したかを格納
        private bool _isFinished;

        //取得したデータを格納するクラス（すべてのデータをここに整理して入れる）
        private IpInfo _ipinfo;



        // Use this for initialization
        void Start()
        {
            _isFinished = false;
            _ipinfo = GetComponent<IpInfo>();
            StartCoroutine(SelectData());
        }

        // Update is called once per frame
        void Update()
        {
        }

        //これにip関係のすべてのデータが入っている
        public IpInfo GetIpInfo()
        {
            return _ipinfo;
        }

        //終わったらtrueを返す
        public bool CheckFinished()
        {
            return _isFinished;
        }

        //もう一度sqlを発行して、新しいデータがあれば取得する
        public IEnumerator ReGetSql()
        {
            //終了フラグをfalseに
            _isFinished = false;
            yield return SelectData();

        }

        //sqlから取得した情報をもとに、緯度と経度を整理し変数に格納する
        //元データ35.690000/139.690000,35.690000/139.690000,35.520600/139.717200,35.690000/139.690000,35.690000/139.690000,51.299300/9.491000,51.299300/9.491000
        //latArray=[35.6900,35.6900,35,6900・・・]
        //latArray=[139.6900,139.6900,139,6900・・・]
        //に直す
        private void AnalyzeLocation(string sqlLocation)
        {
            List<string> latList = new List<string>();
            List<string> lonList = new List<string>();
            //"180/360"みたいに格納される
            string[] routeArray = sqlLocation.Split(',');
            foreach (string latLon in routeArray)
            {
                // "/"で区切って緯度と経度を区別
                //配列の0に緯度、1に経度が格納される
                string[] locationArray = latLon.Split('/');
                string latitude= locationArray[0];
                string longitude = locationArray[1];
                latList.Add(latitude);
                lonList.Add(longitude);
                //緯度経度の経路を格納
            }
            _ipinfo.SetIpRouteLatArray(latList.ToArray());
            _ipinfo.SetIpRouteLonArray(lonList.ToArray());
        }

        public IEnumerator wait()
        {
            while (CheckFinished() == false)
            {
                //sqlの取得が終わるまで待機
                yield return new WaitForSeconds(0.5f);
            }
        }

        //Based from http://qiita.com/oishihiroaki/items/6eb9732efb44d4986428
        private IEnumerator SelectData(){
            MySqlConnection con = null;
            string conCmd =
                "server=" + SERVER + ";" +
                "database=" + DATABASE + ";" +
                "userid=" + USERID + ";" +
                "port=" + PORT + ";" +
                "password=" + PASSWORD;

            try
            {
                con = new MySqlConnection(conCmd);
                con.Open();

            }
            catch (MySqlException ex)
            {
                Debug.Log(ex.ToString());
            }

            string selCmd = "SELECT * FROM " + TABLENAME + " where is_checked = 0 AND location_route.id = ip_infomation.id AND location != \"\" limit 1000;";

            MySqlCommand cmd = new MySqlCommand(selCmd, con);

            IAsyncResult iAsync = cmd.BeginExecuteReader();

            while (!iAsync.IsCompleted)
            {
                yield return 0;
            }

            MySqlDataReader rdr = cmd.EndExecuteReader(iAsync);

            List<int> idList = new List<int>();
            while (rdr.Read())
            {
                idList.Add(rdr.GetInt32("id"));
                ExtructFromSql(rdr);
                //次の列の情報を格納する
                _ipinfo.AddNext();
            }
            rdr.Close();
            rdr.Dispose();
            con.Close();
            con.Dispose();
            yield return StartCoroutine(updateData(idList));
        }

        private IEnumerator updateData(List<int> idList){
            MySqlConnection con = null;
            string conCmd =
                "server=" + SERVER + ";" +
                "database=" + DATABASE + ";" +
                "userid=" + USERID + ";" +
                "port=" + PORT + ";" +
                "password=" + PASSWORD;

            try
            {
                con = new MySqlConnection(conCmd);
                con.Open();

            }
            catch (MySqlException ex)
            {
                Debug.Log(ex.ToString());
            }

            string insertId = "";
            foreach (int id in idList)
            {
                if (insertId != "")
                {
                    insertId = " OR " + insertId;
                }
                insertId = "id = "+id+insertId;

            }
            string selCmd = "update ip_infomation set is_checked = 1 where " + insertId;
            MySqlCommand cmd = new MySqlCommand(selCmd, con);
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            _isFinished = true;
            yield return null;
        }
        //isCHeckedコラムに１を入れる


        //sqlから取得したデータを整理する
        //追加でデータを取得したい場合はここを修正
        private void ExtructFromSql(MySqlDataReader rdr)
        {
            //カラムが存在するか確認
            //locationの値（緯度、経度）を整理して格納
            if (!rdr.IsDBNull(rdr.GetOrdinal("location")))
                AnalyzeLocation(rdr.GetString("location"));

            //ipの送信元を格納
            if (!rdr.IsDBNull(rdr.GetOrdinal("ip_address_from")))
                _ipinfo.SetIpFrom(rdr.GetString("ip_address_from"));

            //ポートの情報を格納
            if (!rdr.IsDBNull(rdr.GetOrdinal("port")))
                _ipinfo.SetPort(rdr.GetInt32("port"));
        }
    }
}

