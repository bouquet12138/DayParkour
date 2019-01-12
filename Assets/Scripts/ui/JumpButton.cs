using UnityEngine;

namespace ui
{
    public class JumpButton : MonoBehaviour
    {
        /// <summary>
        /// 跳跃按钮按下
        /// </summary>
        public void OnJumpDown()
        {
            Hero.SInstance.Jump(); //玩家跳跃
        }
    }
}