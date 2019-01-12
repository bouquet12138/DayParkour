using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    public static Bricks SInstance; //单例模式

    private float mXGridNum; //水平网格数
    
    public GameObject LeftBrick; //左边的砖块
    public GameObject MiddleBrick; //左边的砖块
    public GameObject RightBrick; //左边的砖块
    
    
    

    private void Awake()
    {
        SInstance = this;
        mXGridNum = Screen.width / (Screen.height / 10f); //横向网格数
    }

    /*/// <summary>
    /// 移动砖块
    /// </summary>
    /// <param name="_speed">移动的速度</param>
    public void Remove(float _speed)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i); //得到子transform
            child.position = new Vector2(child.position.x - _speed, child.position.y);

            if (child.position.x < -mXGridNum - 1)
            {
                child.position = new Vector2(child.position.x + 30, child.position.y); //向后移
            }
        }
    }*/
}