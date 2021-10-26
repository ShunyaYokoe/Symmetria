using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;

    /// <summary>
    /// 家具の配列
    /// </summary>
    GameObject[] furniture;

    bool parents_set;

    bool direction;

    int key = 0;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void Player_move()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            key = 1;
            direction = true;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;
            direction = false;
        }
    }
}
