using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymmetryChecker : MonoBehaviour
{
    public float[] x_Multiple;//x座標を入れる箱
    public float[] y_Multiple;//y座標を入れる箱

    public float[] x_total;   //計算したx座標を入れる箱
    public float[] y_total;   //計算したy座標を入れる箱

    Vector2[] pos;            //X座標、Y座標を取得する変数
    public static bool Symmetry_check;
    int symmetry_ct = 0;
    int check_ct = 0;

    private List<GameObject> furnitures;

    // Use this for initialization
    void Start()
    {
        //furnitureタグのオブジェクトをListに格納
        furnitures = new List<GameObject>(GameObject.FindGameObjectsWithTag("furniture"));
        
        //オブジェクトの名前順に並び替え  
        furnitures.Sort((a, b) => a.name.CompareTo(b.name));
        Symmetry_check = false;
    }

    // Update is called once per frame
    void Update()
    {
        check_ct++;
        //check_countが30ごとにSymmetryメソッドを呼ぶ
        if (check_ct == 30)
        {
            Symmetry();
            check_ct = 0;
        }
      
    }

    /// <summary>
    /// シンメトリー判定
    /// </summary>
    void Symmetry()
    {
        symmetry_ct = 0;
        pos = new Vector2[furnitures.Count];     //furnituresの座標を取得
        x_Multiple = new float[furnitures.Count];//furnituresのx座標を取得
        y_Multiple = new float[furnitures.Count];//furnituresのy座標を取得
        x_total = new float[furnitures.Count];   //furnituresのx座標の計算結果を表示
        y_total = new float[furnitures.Count];   //furnituresのy座標の計算結果を表示

        //x座標とy座標の座標を毎回取得
        for (int i = 0; i < furnitures.Count; i++)
        {
            pos[i] = furnitures[i].transform.position;
            furnitures[i].transform.position = new Vector2(pos[i].x, pos[i].y);
            x_Multiple[i] = pos[i].x;
            y_Multiple[i] = pos[i].y;
        }

        //x座標の判定
        for (int j = 0; j < furnitures.Count; j++)
        {
            if (j % 2 == 0)
            {
                x_total[j] = x_Multiple[j] + x_Multiple[j + 1];
            }
        }
        for (int k = 0; k < x_total.Length; k++)
        {
            if (-0.1f <= x_total[k] && x_total[k] <= 0.1)//-0.1または0.1の誤差内であればシンメトリーになる
            {
                symmetry_ct++;
            }
        }

        //y座標の判定
        for (int l = 0; l < furnitures.Count; l++)
        {
            if (l % 2 == 0)
            {
                y_total[l] = y_Multiple[l] - y_Multiple[l + 1];
            }
        }
        for (int m = 0; m < y_total.Length; m++)
        {
            if (-0.1f <= y_total[m] && y_total[m] <= 0.1)//-0.1または0.1の誤差内であればシンメトリーになる
            {
                symmetry_ct++;
            }
        }

        //シンメトリーか確認
        if (symmetry_ct == x_total.Length + y_total.Length)
        {
            Symmetry_check = true;
            Debug.Log("シンメトリ");
        }
    }
}
