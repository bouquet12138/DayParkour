using UnityEngine;

namespace map
{
    public abstract class BaseMap : MonoBehaviour
    {
        protected int[,] BrickArr; //砖块数组

        protected float XGridNum; //水平网格数

        protected int NowIndex = 0; //当前显示出来的最大索引

        /// <summary>
        /// 唤醒的时候
        /// </summary>
        protected virtual void Awake()
        {
            XGridNum = Screen.width / (Screen.height / 10f); //横向网格数
        }

        /// <summary>
        /// 移动砖块
        /// </summary>
        /// <param name="_nowX">当前x</param>
        /// <param name="_speed">移动速度</param>
        public abstract void Remove(float _nowX, float _speed);

        protected abstract void Generate(float _nowX);
    }
}