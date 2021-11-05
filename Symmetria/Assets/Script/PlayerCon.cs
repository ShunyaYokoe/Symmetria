﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float walkforce = 20.0f;
    float maxwalkspeed = 1.5f;
    /// <summary>
    /// 歩く時のSEの再生間隔を図る変数
    /// </summary>
    float walk_ct = 0;

    /// <summary>
    /// 家具の配列
    /// </summary>
    GameObject[] furnitures;

    /// <summary>
    /// 親子関係かどうか見るためのbool
    /// </summary>
    bool parents_set;

    /// <summary>
    /// 家具に触れているかどうか
    /// </summary>
    bool furnitures_touch;

    int key = 0;

    void Start()
    {
        furnitures = GameObject.FindGameObjectsWithTag("furniture");
        furnitures_touch = false;
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        parents_set = false;
    }

    void Update()
    {
        Player_move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    void Player_move()
    {
        walk_ct = 0;
        if(Input.GetKey(KeyCode.RightArrow))
        {        
            key = 1;
            Walk_SE();
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;
            Walk_SE();
        }

        //歩く
        rigid2D.AddForce(transform.right * key * walkforce);

        //反転
        if (key != 0)
        {
            if (!parents_set)
            {
                //親子関係じゃないとき
                transform.localScale = new Vector3(key, 1, 1);
            }
            else
            {
                //親子関係の時プレイヤーに合わせて向きを変える
            }
          
        }
        else
        {
            //入力していないとき
            rigid2D.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
    }

    /// <summary>
    /// 家具を持つ
    /// </summary>
    /// <param name="h_furnitures">触れている家具</param>
    void Have_furnitures(GameObject h_furnitures)
    {
        //家具に触れているとき
        if (furnitures_touch)
        {
            //親子関係じゃないとき
            if (!parents_set)
            {
                parents_set = true;
                Debug.Log("持つ");
                //親子関係にする
                h_furnitures.transform.parent = transform;
                //レイヤーを上げる
                h_furnitures.GetComponent<Renderer>().sortingOrder = 5;
                //子オブジェクトのポジションを親オブジェクトの横にずらす
                h_furnitures.transform.position
                       = new Vector3(transform.position.x + 0.2f, h_furnitures.transform.position.y, transform.position.z);
            }
            else
            {
                parents_set = false;
                Debug.Log("降ろす");
            }
        }
    }

    /// <summary>
    /// 歩く時のSE再生
    /// </summary>
    void Walk_SE()
    {
        walk_ct++;
        if (walk_ct >= 10)
        {
            //歩くSE再生
            Debug.Log("SE再生");
            walk_ct = 0;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag=="Door")
        {
            //ドアを開くメソッド起動
        }

        if(col.gameObject.tag== "furnitures"||col.gameObject.tag== "furnitures_mirror")
        {
            furnitures_touch = true;
        }
        else
        {
            furnitures_touch = false;
        }
    }

    /*
     *void Update()
     *{
     *   zボタン押す｛家具持つメソッド起動｝
     *}
     * 
     *void 家具持つメソッド()
     *{
     * if(kagu_touch){親子関係にする}
     * 
     * if（親子関係の時）
     * {
     * 　if(家具に触れていないとき)
     *  {
     *   降ろす
     *  }
     * }
     * 
     *} 
     * 
     *void 当たり判定メソッド
     *{
     * 
     * kagu_touch=true;
     * 
     *}
     */
}
