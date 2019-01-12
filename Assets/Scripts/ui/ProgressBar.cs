using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class ProgressBar : MonoBehaviour
    {
        public static ProgressBar SInstance;
        private RectTransform mMaskBar; //遮罩bar
        private Text mProgressText; //进度文本
        private float mTotalWidth; //总宽
        private int mNowPercent; //当前百分比

        private void Awake()
        {
            SInstance = this; //初始化自己
        }

        /// <summary>
        /// 唤醒的时候
        /// </summary>
        private void Start()
        {
            mMaskBar = transform.GetChild(0).GetComponent<RectTransform>(); //遮罩组件
            mProgressText = transform.GetChild(1).GetComponent<Text>(); //得到文本

            mTotalWidth = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta.x; //总宽度
            Debug.Log("mTotalWidth " + mTotalWidth);
            SetNowPercent(0); //设置当前percent 为0
        }

        /// <summary>
        /// 设置当前百分比
        /// </summary>
        /// <param name="_percent">百分比</param>
        public void SetNowPercent(float _percent)
        {
            if (mNowPercent != (int) (_percent * 100))
            {
                mNowPercent = (int) (_percent * 100);
                mMaskBar.sizeDelta = new Vector2(mTotalWidth * _percent, mMaskBar.sizeDelta.y); //设置遮罩组件的宽度
                mProgressText.text = mNowPercent + "%"; //设置百分比文本
            }
        }
    }
}