using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float mXGridNum; //水平网格数


    private readonly List<Transform> mLongTransforms = new List<Transform>(); //远景每个物体的transform
    private readonly List<Transform> mMidTransforms = new List<Transform>(); //中景每个物体的transform
    private readonly List<Transform> mCloseTransforms = new List<Transform>(); //近景每个物体的transform

    public GameObject LongRangePrefab; //远景物体
    public GameObject MidRangePrefab; //中景草地
    public GameObject CloseRangePrefab; //近景

    public static Background SInstance; //单例模式

    // private float mNowX; //当前x
    private float mLongWidth; //远景宽
    private float mMidWidth; //中景宽
    private float mCloseWidth; //近景宽

    /// <summary>
    /// 唤醒的时候
    /// </summary>
    private void Awake()
    {
        SInstance = this; //初始化一下

        mXGridNum = Screen.width / (Screen.height / 10f); //x方向的网格数

        mLongWidth = LongRangePrefab.GetComponent<Renderer>().bounds.size.x;
        mMidWidth = MidRangePrefab.GetComponent<Renderer>().bounds.size.x;
        mCloseWidth = CloseRangePrefab.GetComponent<Renderer>().bounds.size.x;

        Debug.Log("mXGridNum " + mXGridNum);

        Debug.Log("mLongWidth " + mLongWidth);
        Debug.Log("mMidWidth " + mMidWidth);
        Debug.Log("mCloseWidth " + mCloseWidth);

        InitPrefabs(LongRangePrefab, mLongWidth, 0, mLongTransforms); //初始化远景
        InitPrefabs(MidRangePrefab, mMidWidth, 1, mMidTransforms); //初始化中景
        InitPrefabs(CloseRangePrefab, mCloseWidth, 2, mCloseTransforms); //初始化近景
    }


    /// <summary>
    /// 移动背景
    /// </summary>
    public void Remove(float _nowX)
    {
        float x1 = (_nowX * 0.1f) % ((mLongTransforms.Count - 1) * (mLongWidth - 0.02f))
                   - (mXGridNum - mLongWidth) / 2;
        foreach (Transform t in mLongTransforms)
        {
            if (x1 < -(mXGridNum + mLongWidth) / 2)
            {
                float longWidth = mLongTransforms.Count * (mLongWidth - 0.02f);
                t.position = new Vector2(x1 + longWidth, t.position.y);
            }
            else
            {
                t.position = new Vector2(x1, t.position.y); //直接设上位置
            }

            x1 += mLongWidth - 0.02f;
        }


        float x2 = (_nowX / 3) % ((mMidTransforms.Count - 1) * (mMidWidth - 0.02f))
                   - (mXGridNum - mMidWidth) / 2;
        foreach (Transform t in mMidTransforms)
        {
            if (x2 < -(mXGridNum + mMidWidth) / 2)
            {
                float longWidth = mMidTransforms.Count * (mMidWidth - 0.02f);
                t.position = new Vector2(x2 + longWidth, t.position.y);
            }
            else
            {
                t.position = new Vector2(x2, t.position.y); //直接设上位置
            }

            x2 += mMidWidth - 0.02f;
        }

        float x3 = (_nowX * 0.8f) % ((mCloseTransforms.Count - 1) * (mCloseWidth - 0.02f))
                   - (mXGridNum - mCloseWidth) / 2;
        foreach (Transform t in mCloseTransforms)
        {
            if (x3 < -(mXGridNum + mCloseWidth) / 2)
            {
                float longWidth = mCloseTransforms.Count * (mCloseWidth - 0.02f);
                t.position = new Vector2(x3 + longWidth, t.position.y);
            }
            else
            {
                t.position = new Vector2(x3, t.position.y); //直接设上位置
            }

            x3 += mCloseWidth - 0.02f;
        }
    }


    /// <summary>
    /// 初始化预制体
    /// </summary>
    /// <param name="_prefab">预制体对象</param>
    /// <param name="_prefabWidth">预制体宽</param>
    /// <param name="_parentIndex">父类索引</param>
    /// <param name="_transforms"></param>
    private void InitPrefabs(GameObject _prefab, float _prefabWidth, int _parentIndex, List<Transform> _transforms)
    {
        if (_prefab == null)
            return; //为空直接返回

        Transform parentTransform = transform.GetChild(_parentIndex); //父亲位置

        float x = -mXGridNum / 2 + _prefabWidth / 2;
        float y = _prefab.transform.position.y; //y的位置
        float z = _prefab.transform.position.z;

        int backGroundNum = (int) (mXGridNum / _prefabWidth) + 2;

        for (int i = 0; i < backGroundNum; i++)
        {
            GameObject instantPrefab = Instantiate(_prefab); //实例化一个背景
            instantPrefab.transform.position = new Vector3(x, y, z); //位置
            instantPrefab.transform.parent = parentTransform; //设置一下父亲
            _transforms.Add(instantPrefab.transform); //存储起来

            x += _prefabWidth - 0.02f; //向左偏
        }
    }
}