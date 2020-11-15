using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////
    /////// ★あり…「発展課題：ユニティちゃんの位置に応じてアイテムを動的に生成しよう」の加筆部分 ///////
    //////////////////////////////////////////////////////////////////////////////////////////////////////

    // carPrefabを入れる
    public GameObject carPrefab;
    // coinPrefabを入れる
    public GameObject coinPrefab;
    // cornPrefabを入れる
    public GameObject conePrefab;
    // スタート地点
    private int startPos = 80;
    // ゴール地点
    private int goalPos = 360;
    // アイテムを出すX方向の範囲
    private float posRange = 3.4f;
    // ★unitychanのtransform
    private Transform unitychanTF;
    // ★unitychanのZ座標
    private float unitychanZ;
    // ★画面内判定距離；画面内の範囲にインスタンスを順次生成するための設定距離
    private float visibleDis = 60;
    // ★Item生成の開始
    private bool generating = false;
    // ★Itemを生成する間隔
    private float itemInterval = 15;
    // ★次のItem生成を実行するための必要距離
    private float disCount = 0;
    // ★前フレームのunitychanのZ座標位置
    private float preUnitychanZ;

    // Start is called before the first frame update
    void Start()
    {
        // ★unitychanのTransformを取得
        unitychanTF = GameObject.Find("unitychan").transform;

        // ★preUnitychanZの初期値を代入
        preUnitychanZ = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        // ★unitychanの現在のZ座標を取得
        this.unitychanZ = unitychanTF.position.z;

        // ★画面内判定距離を減算したスタート地点とゴール地点の範囲内でItem生成を実行する
        if (unitychanZ >= startPos - visibleDis && unitychanZ < goalPos - visibleDis)
        {
            // ★Item生成の開始
            generating = true;
        }
        else if(unitychanZ >= goalPos - visibleDis)
        {
            // ★Item生成の終了
            generating = false;
        }

        // ★Item生成の実行
        if (generating)
        {
            CountDistance();
        }
    }

    // ★設定間隔ごとにItem生成を実行
    void CountDistance()
    {
        // ★次のItem生成を実行するための必要距離disCount（累積値）に、前フレーム時点からのZ座標移動距離を加算する
        disCount += unitychanZ - preUnitychanZ;
        // ★preUnitychanZの更新
        preUnitychanZ = unitychanZ;

        // ★disCountがItemを生成する間隔距離itemIntervalを超過した場合、Itemを生成する
        if (disCount >= itemInterval)
        {
            // ★Itemを生成する位置
            float generatePos = unitychanZ + visibleDis;
            // ★Itemの生成
            GenerateItems(generatePos);
            // ★disCountのリセット
            disCount = 0;
        }
    }

    // ★Itemの生成
    void GenerateItems(float gP)
    {
        // どのアイテムを出すのかをランダムに設定
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            // コーンをX軸方向に一直線に生成
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, gP); // ★Z座標の変更
            }
        }
        else
        {
            // レーンごとにアイテムを生成
            for (int j = -1; j <= 1; j++)
            {
                // アイテムの種類を決める
                int item = Random.Range(1, 11);
                // アイテムを置くZ座標のオフセットをランダムに設定
                int offsetZ = Random.Range(-5, 6);
                // 60%コイン配置 : 30%車配置 : 10%何もなし
                if (1 <= item && item <= 6)
                {
                    // コインを生成
                    GameObject coin = Instantiate(coinPrefab);
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, gP + offsetZ); // ★Z座標の変更
                }
                else if (7 <= item && item <= 9)
                {
                    // 車を生成
                    GameObject car = Instantiate(carPrefab);
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, gP + offsetZ); // ★Z座標の変更
                }
            }
        }
    }

}
