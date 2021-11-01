using System.Collections;
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

    int key = 0;

    void Start()
    {
        furnitures = GameObject.FindGameObjectsWithTag("furniture");

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
        if(key!=0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //animator.speed=
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
}
