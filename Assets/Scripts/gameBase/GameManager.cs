using map;
using ui;
using UnityEngine;

namespace gameBase
{
    public class GameManager : MonoBehaviour
    {
        private float mXGridNum; //水平网格数

        private float mNowDistance; //当前距离
        private float mTotalDistance; //总距离
        private float mSpeed = 0.1f; //移动速度

        /// <summary>
        /// 唤醒的时候
        /// </summary>
        private void Awake()
        {
            mXGridNum = Screen.width / (Screen.height / 10f); //横向网格数
            mTotalDistance = MapManager.Instance.Width * 0.4f; //总距离
        }

        /// <summary>
        /// 每次更新
        /// </summary>
        private void Update()
        {
            mNowDistance += mSpeed; //NowDistance 加加

            if (mNowDistance <= 5)
            {
                Hero.Instance.RemoveHero(mNowDistance); //移动英雄   
            }
            else if (mNowDistance < mTotalDistance - mXGridNum + 5)
            {
                Background.SInstance.Remove(-mNowDistance + 5); //移动背景
                BrickMap.Instance.Remove(mNowDistance - 5, mSpeed); //移动砖块
                GoldMap.Instance.Remove(mNowDistance - 5, mSpeed); //移动金币
                Hero.Instance.CheckHeroPosition(5, mSpeed / 2);
            }
            else if (mNowDistance < mTotalDistance)
            {
                Hero.Instance.RemoveHero(mNowDistance - (mTotalDistance - mXGridNum)); //移动英雄   
            }
            else //最后
            {
            }

            if (mNowDistance < mTotalDistance)
            {
                Distance.SInstance.SetNowDistance((int) mNowDistance); //将距离传进去
                ProgressBar.SInstance.SetNowPercent(mNowDistance / mTotalDistance); //设置进度条百分比
            }
        }
    }
}