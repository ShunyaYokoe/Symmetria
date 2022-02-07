using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerCon : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;

    public PlayerData playerdata;

    ///// <summary>
    ///// 移動加速度
    ///// </summary>
    //float walkforce = 1000.0f;

    ///// <summary>
    ///// 最高速度
    ///// </summary>
    //float maxwalkspeed = 1.5f;

    /// <summary>
    /// 歩く時のSEの再生間隔を図る変数
    /// </summary>
    float walk_ct = 0;

    /// <summary>
    /// 家具の配列
    /// </summary>
    GameObject[] furnitures;

    /// <summary>
    /// 触れている家具
    /// </summary>
    GameObject t_furniture;

    /// <summary>
    /// 持っている家具
    /// </summary>
    GameObject h_furniture;

    /// <summary>
    /// 親子関係かどうか見るためのbool
    /// </summary>
    bool parents_set;

    /// <summary>
    /// 家具に触れているかどうか
    /// </summary>
    bool furnitures_touch;

    /// <summary>
    /// プレイヤーの向き(右：１　左：－１　入力なし：０)
    /// </summary>
    float player_dire;

    /// <summary>
    /// 最後に向いていたプレイヤーの向き
    /// </summary>
    float last_pl_dire;

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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //親子関係じゃないとき(家具を持っていないとき)
            if (!parents_set)
            {
                //家具に触れているとき
                if (furnitures_touch)
                {
                    Have_furniture();
                }
            }
            else
            {
                if (!furnitures_touch)
                {
                    Drop_furniture();
                }
            }
        }
    }

    /// <summary>
    /// 左右移動＆待機モーション
    /// </summary>
    void Player_move()
    {
        player_dire = 0;

        walk_ct = 0;

        player_dire = Input.GetAxisRaw("Horizontal");
        player_dire *= -1.0f;

        //現在の速度
        float pl_speed = Mathf.Abs(rigid2D.velocity.x);

        //アニメーション＆SE起動
        if (pl_speed > 0.1)
        {
            animator.SetFloat("WaitFloat", pl_speed);
            Walk_SE();
        }
        else
        {
            animator.SetFloat("WaitFloat", 0.0f);
        }

        //速度に応じてアニメーション速度を変える
        animator.speed = pl_speed / 2.0f;

        if (player_dire != 0)
        {
            last_pl_dire = player_dire;
            //速度制限
            if (pl_speed < playerdata.maxwalkspeed)
            {
                //歩く
                rigid2D.AddForce(transform.right * player_dire * playerdata.walkforce * Time.deltaTime);
            }

            //反転
            transform.localScale = new Vector3(player_dire, 1, 1);
        }
        else
        {
            //入力していないとき
            rigid2D.velocity = Vector2.zero;
            animator.SetFloat("WaitFloat", 0.0f);
        }
    }

    /// <summary>
    /// 家具を持つ
    /// </summary>
    void Have_furniture()
    {
        h_furniture = t_furniture;
        parents_set = true;

        Debug.Log("持つ");
        //親子関係にする
        h_furniture.transform.parent = transform;
        //レイヤーを上げる
        h_furniture.GetComponent<Renderer>().sortingOrder = 5;
        //子オブジェクトのポジションを親オブジェクトの横にずらす
        h_furniture.transform.position
               = new Vector3(transform.position.x + (0.5f * last_pl_dire), h_furniture.transform.position.y, transform.position.z);
    }

    /// <summary>
    /// 家具を降ろす
    /// </summary>
    void Drop_furniture()
    {
        h_furniture.transform.parent = null;
        parents_set = false;
        //レイヤーを下げる
        h_furniture.GetComponent<Renderer>().sortingOrder = 1;
        Debug.Log("降ろす");
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
        if (col.gameObject.tag == "Door")
        {
            //ドアを開くメソッド起動
        }

        if (col.gameObject.tag == "furniture" || col.gameObject.tag == "furniture_mirror")
        {
            furnitures_touch = true;
            t_furniture = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "furniture" || col.gameObject.tag == "furniture_mirror")
        {
            furnitures_touch = false;
            t_furniture = null;
        }
    }
}
