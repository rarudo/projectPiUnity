using System.Collections;
using UnityEngine;

//MysqlBase
//IpInfo
//この２つを同じオブジェクト内に入れて使用してください
//上の２つのクラスでsql関係の処理は完了しているので
//簡単な使用例を、サンプルプログラムとして記述します

//namespace Assets.Scripts
//{
//    public class SqlTest : MonoBehaviour
//    {
//        // Use this for initialization
//        void Start ()
//        {
//            StartCoroutine(Loop());
//        }
//
//
//        IEnumerator Loop()
//        {
//            while (true)
//            {
//                //1コルーチン　＝　１フレームに１パケットなので
//                //３つ並列に動かして１フレームに３パケット射出する
//                //重いときは減らす
//                StartCoroutine(SqlGetTes());
//                yield return null;
//                StartCoroutine(SqlGetTes());
//                yield return null;
//                yield return StartCoroutine(SqlGetTes());
//                yield return null;
//            }
//        }
//
//        IEnumerator SqlGetTes()
//        {
//            MysqlBase sqlBase =GetComponent<MysqlBase>();
//            Scanner scanner = GetComponent<Scanner>();
//            //sqlの取得が終わるまで待機
//            yield return sqlBase.wait();
//            //IpInfo型で結果をもらう
//            IpInfo iftes = sqlBase.GetIpInfo();
//
//            //この中がメインの処理
//            //fetchメソッドが実行されるたびに、新しいIPの情報に切り替わる
//            Vector2 vec2tes;
//            while (iftes.Fetch())
//            {
//                vec2tes.x = (float) iftes.GetLonFrom();
//                vec2tes.y = (float) iftes.GetLatFrom();
//                yield return null;
//                //yield return new WaitForSeconds(0.0001f);
//                scanner.Shot(vec2tes);
//                //ここで1IPアドレスに対する情報を取得
//                //Debug.Log("ip address" + iftes.GetIpFrom());
//                //Debug.Log("緯度" + iftes.GetLatFrom());
//                //Debug.Log("経度" + iftes.GetLonFrom());
//                //Debug.Log("ポート" + iftes.GetPort());
//                //次のループでは別のipの情報が入る
//            }
//                StartCoroutine(sqlBase.ReGetSql());
//        }
//        // Update is called once per frame
//                void Update () {
//
//        }
//    }
//}
