using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////////////////////
    /////// ◆あり…「課題：不要になったアイテムを順次破棄しよう」の加筆部分 ///////
    ////////////////////////////////////////////////////////////////////////////////

    // ◆unitychanのtransform
    private Transform unitychanTF;
    // ◆unityちゃんとDestroyCubeとの距離
    private float difference;

    // Start is called before the first frame update
    void Start()
    {
        // ◆unitychanのTransformを取得
        unitychanTF = GameObject.Find("unitychan").transform;
        // ◆unityちゃんとDestroyCubeとのZ座標距離の差を求める
        this.difference = unitychanTF.position.z - this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // ◆Unityちゃんの位置に合わせてDestroyCubeの位置を移動
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, unitychanTF.position.z - this.difference);
    }

    // ◆トリガーモードで他のオブジェクトと接触した場合の処理
    void OnTriggerEnter(Collider other)
    {
        // ◆障害物に衝突した場合
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag" || other.gameObject.tag == "CoinTag")
        {
            // ◆接触したオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }
}
