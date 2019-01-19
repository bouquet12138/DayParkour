using gameBase;
using ui;
using UnityEngine;

namespace good
{
    public class Jelly : MonoBehaviour
    {
        public int AddScore; //添加的分数
        public AudioClip AddScoreAudio; //添加分数的声音
        public GameObject DestroyAnim; //销毁动画


        /// <summary>
        /// 碰撞器进入
        /// </summary>
        /// <param name="_other"></param>
        private void OnTriggerEnter2D(Collider2D _other)
        {
            if (_other.gameObject.tag.Equals("Player"))
            {
                ExpressionScore.SInstance.AddScore(AddScore); //加分	
                SoundEffectController.PlaySound(AddScoreAudio, transform.position); //播放吃金币声音

                if (DestroyAnim != null) //如果销毁动画不是空
                    Instantiate(DestroyAnim, transform.position, Quaternion.identity); //生成加分效果

                Destroy(gameObject); //销毁自己
            }
        }
    }
}