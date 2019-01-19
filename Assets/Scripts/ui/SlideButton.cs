using UnityEngine;
using UnityEngine.EventSystems;

namespace ui
{
    public class SlideButton : MonoBehaviour, IPointerExitHandler
    {
        /// <summary>
        /// 跳跃按钮按下
        /// </summary>
        public void OnSlideDown()
        {
            Hero.Instance.Slide(); //玩家下滑
        }

        /// <summary>
        /// 手指离开
        /// </summary>
        /// <param name="_eventData"></param>
        public void OnPointerExit(PointerEventData _eventData)
        {
            Hero.Instance.SlideRestore(); //下滑恢复 
        }
    }
}