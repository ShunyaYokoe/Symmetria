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
    /// 家具の配列
    /// </summary>
    GameObject[] furnitures;
    bool parents_set;
    bool player_dire;

    int key = 0;

    void Start()
    {
        furnitures = GameObject.FindGameObjectsWithTag("furniture");

        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        parents_set = false;
        player_dire = false;
    }

    void Update()
    {

    }

    void Player_move()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            key = 1;
            player_dire = true;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;
            player_dire = false;
        }
    }
}
