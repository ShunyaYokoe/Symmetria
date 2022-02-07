using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseSystem : MonoBehaviour
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    GameObject player;
    /// <summary>
    /// プレイヤー
    /// </summary>
    GameObject m_player;
    /// <summary>
    /// 入れ替わりの判定
    /// </summary>
    GameObject portal;
    /// <summary>
    /// 入れ替わりの判定（鏡側）
    /// </summary>
    GameObject m_portal;

    bool judge;
    bool portal_check;
    bool m_portal_check;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        m_player = GameObject.Find("M_Player");
        portal = GameObject.Find("Portal");  
        m_portal = GameObject.Find("Portal_2");
        judge = false;
        portal_check = false;
        m_portal_check = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(portal_check&&m_portal_check)
        {
            judge = true;
        }
        else
        {
            judge = false;
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            if (judge)
            {
                Change_player();
            }
        }
    }

    void Change_player()
    {
        //プレイヤーの座標入れ替え
        player.transform.position = m_portal.transform.position;
        m_player.transform.position = portal.transform.position;
        //プレイヤーの入れ替え    
        GameObject player_box;
        player_box = player;
        player = m_player;
        m_player = player_box;

        judge = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            portal_check = true;
        }
        else
        {
            portal_check=false;
        }
        if (col.gameObject.tag == "M_Player")
        {
            m_portal_check = true;
        }
        else
        {
            m_portal_check = false;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            portal_check = true;
        }
        if (col.gameObject.tag == "M_Player")
        {
            m_portal_check = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        portal_check = false;
        m_portal_check = false;
    }
}
