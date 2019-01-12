using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class ExpressionScore : MonoBehaviour
    {
        public List<Sprite> SpriteNumList; //数字列表

        public GameObject NumObject; //用来表示数字的对象

        private int mNowScore; //成绩

        private int mMaxDigit; //最大位数 默认为 0
        private float mNumWidth; //当前数字的宽度


        public static ExpressionScore SInstance;

        private void Awake()
        {
            SInstance = this;
            mNumWidth = NumObject.GetComponent<RectTransform>().sizeDelta.x; //数字的宽
            AddScore(0); //初始化一个0
        }

        /// <summary>
        /// 添加分数 
        /// </summary>
        public void AddScore(int _score)
        {
            mNowScore += _score;
            AddScoreImage(); //添加分数
        }

        /// <summary>
        /// 添加分数图片
        /// </summary>
        private void AddScoreImage()
        {
            String scoreStr = mNowScore + "";

            int digit = scoreStr.Length; //成绩的位数
            if (digit > mMaxDigit)
            {
                mMaxDigit = digit;
            }

            for (int i = transform.childCount; i < digit; i++)
            {
                GameObject numImage = Instantiate(NumObject, transform); //生成一个数字
                numImage.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(-i * mNumWidth * 0.85f, -3); //设置数字的位置
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
        public int GetNowScore()
        {
            return mNowScore;
        }
    }
}