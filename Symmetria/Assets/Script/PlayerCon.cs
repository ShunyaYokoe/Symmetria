using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float walkForce;
    float maxWalkSpeed;

    /// <summary>
    /// プレイヤーの向き
    /// </summary>
    int key = 0;


    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 移動
    /// </summary>
    void PlayerMove()
    {
        //待機
        if(key==0)
        {

        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            key = 1;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;
        }
    }
}
