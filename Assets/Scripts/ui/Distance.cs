using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class Distance : MonoBehaviour
    {
        public List<Sprite> SpriteNumList; //数字列表

        public GameObject NumObject; //用来表示数字的对象

        private int mNowDistance; //成绩

        private int mMaxDigit; //最大位数 默认为 0
        private float mNumWidth; //当前数字的宽度


        public static Distance SInstance;

        private void Awake()
        {
            SInstance = this;
            mNumWidth = NumObject.GetComponent<RectTransform>().sizeDelta.x; //数字的宽
            SetNowDistance(0); //初始化一个0
        }

        /// <summary>
        /// 设置当前距离
        /// </summary>
        public void SetNowDistance(int _nowDistance)
        {
            if (mNowDistance != _nowDistance)
            {
                mNowDistance = _nowDistance;
                AddDistanceImage(); //添加分数
            }
        }

        /// <summary>
        /// 添加距离图片
        /// </summary>
        private void AddDistanceImage()
        {
            String scoreStr = mNowDistance + "";

            int digit = scoreStr.Length; //成绩的位数
            if (digit > mMaxDigit)
            {
                mMaxDigit = digit;
            }

            for (int i = transform.childCount; i < digit; i++)
            {
                GameObject numImage = Instantiate(NumObject, transform); //生成一个数字
                numImage.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(-i * mNumWidth * 0.85f, -2); //设置数字的位置
            }

            for (int i = 0; i < digit; i++)
            {
                String numStr = scoreStr.Substring(i, 1);
                int numInt = int.Parse(numStr); //解析一下数字
                transform.GetChild(digit - 1 - i).GetComponent<Image>().overrideSprite = SpriteNumList[numInt]; //设置一下数字
            }
        }

        /// <summary>
        /// 得到当前成绩
        /// </summary>
        /// <returns></returns>
        public int GetNowDistance()
        {
            return mNowDistance;
        }
    }
}