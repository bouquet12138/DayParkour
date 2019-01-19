using gameBase;
using ui;
using UnityEngine;

namespace good
{
    public class Gold : MonoBehaviour
    {
        private int mAddScore; //加的分

        public GameObject DestroyAnim; //销毁动画
        public AudioClip EatGoldClip; //吃金币的音效

        /// <summary>
        /// 设置金币类型
        /// </summary>
        /// <param name="_goldImage">金币图片</param>
        /// <param name="_addScore">添加的分数</param>
        public void SetType(Sprite _goldImage, int _addScore)
        {
            mAddScore = _addScore;
            GetComponent<SpriteRenderer>().sprite = _goldImage;
        }


        /// <summary>
        /// 碰撞器进入
        /// </summary>
        /// <param name="_other"></param>
        private void OnTriggerEnter2D(Collider2D _other)
        {
            if (_other.gameObject.tag.Equals("Player"))
            {
                ExpressionScore.SInstance.AddScore(mAddScore); //加分	
                SoundEffectController.PlaySound(EatGoldClip, transform.position); //播放吃金币声音
                Instantiate(DestroyAnim, transform.position, Quaternion.identity); //生成加分效果
                Destroy(gameObject); //销毁自己
            }
        }
    }
}