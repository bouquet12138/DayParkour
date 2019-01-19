using UnityEngine;

namespace gameBase
{
    // 文件名称：UserInput.cs
    // 功能描述：音效的基础控制类
    // 编写作者：小韩
    // 编写日期：2019.1.18
    public static class SoundEffectController
    {
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="_audio">要播放的音效</param>
        /// <param name="_position">位置</param>
        public static void PlaySound(AudioClip _audio, Vector3 _position)
        {
            AudioSource.PlayClipAtPoint(_audio, _position); //在此处播放音效
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="_audio">要播放的音效</param>
        public static void PlaySound(AudioClip _audio)
        {
            AudioSource.PlayClipAtPoint(_audio, new Vector3(0, 0, 0)); //在此处播放音效
        }
    }
}