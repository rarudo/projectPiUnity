using System.Collections.Generic;
using UnityEngine;

//1つ目のipのデータ
//    |-string ipFrom
//    |-string ipTo
//    |-string[] ipRoute
//    |-int port
//    |-string service
//2つ目のipのデータ
//    |-string ipFrom
//    |-string ipTo
//    |-string[] ipRoute
//    |-int port
//    |-string service

//fetchメソッドを呼ぶと次のデータへ


namespace Assets.Scripts
{
    public class IpInfo : MonoBehaviour
    {
        private List<IpStruct> ipInfoList = new List<IpStruct>();
        struct IpStruct
        {
            public string IpFrom;
            public string IpTo;
            public string[] IpLatArray;
            public string[] IpLonArray;
            public int Port;
            public string Service;
        }
        private IpStruct _ipInfoStruct;
        private int _nowOperator = -1;
        //データを取り出すときに使用、ここに1ipアドレスすべての情報が入る
        private IpStruct _nowIpStruct;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        //アクセスしてきたポート番号を返す
        public int GetPort()
        {
            return _nowIpStruct.Port;
        }

        //ポートから、どのサービスにアクセスしてきたのかを返す
        public string GetService()
        {
            //TODO
            return "";
        }

        //辿ってきた経路の緯度を配列で返す
        public string[] GetIpLatArray()
        {
            return _nowIpStruct.IpLatArray;
        }

        //辿ってきた経路の経度を配列で返す
        public string[] GetIpLonArray()
        {
            return _nowIpStruct.IpLonArray;
        }

        //送信元ipアドレスを返す
        public string GetIpFrom()
        {
            return _nowIpStruct.IpFrom;
        }

        //ipパケットの出発点（緯度）を返す
        public double GetLatFrom()
        {
            //配列の最後の要素を習得
            string latitudeStr = _nowIpStruct.IpLatArray[_nowIpStruct.IpLatArray.Length-1];
            return double.Parse(latitudeStr);
        }

        //ipパケットの出発点（経度）を返す
        public double GetLonFrom()
        {
            //配列の最後の要素を習得
            string longitudeStr = _nowIpStruct.IpLonArray[_nowIpStruct.IpLonArray.Length-1];
            return double.Parse(longitudeStr);
        }

        public void SetIpFrom(string ip)
        {
            _ipInfoStruct.IpFrom = ip;
        }

        public void SetIpTo(string ip)
        {
            _ipInfoStruct.IpTo= ip;
        }

        public void SetIpRouteLatArray(string[] ipLatArray)
        {
            _ipInfoStruct.IpLatArray = ipLatArray;
        }

        public void SetIpRouteLonArray(string[] ipLonArray)
        {
            _ipInfoStruct.IpLonArray = ipLonArray;
        }

        public void SetPort(int port)
        {
            _ipInfoStruct.Port = port;
        }

        public void SetService(string service)
        {
            _ipInfoStruct.Service = service;
        }


        private bool CheckNotNull()
        {
            //最低限必要なものだけ今はチェック
            if (_ipInfoStruct.IpFrom == "")
            {
                Debug.LogError("ipInfo構造体書き込みエラー ");
                return false;
            }
            return true;
        }

        private void SetNowIpStruct()
        {
            _nowIpStruct = ipInfoList[_nowOperator];
        }

        public void AddNext()
        {
            if (CheckNotNull())
                ipInfoList.Add(_ipInfoStruct);
            //構造体の中身をカラに
            _ipInfoStruct = default(IpStruct);
        }

        public bool Fetch()
        {
            //格納数のチェック
            if (_nowOperator +1 < ipInfoList.Count)
            {
                _nowOperator++;
                //次の構造体をセット
                SetNowIpStruct();
                return true;
            }
            return false;
        }
    }
}
