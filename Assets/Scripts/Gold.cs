using System.Collections;
using System.Collections.Generic;
using gameBase;
using ui;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int AddScore; //加的分
    public GameObject DestroyAnim; //销毁动画
    public AudioClip EatGoldClip; //吃金币的音效


    /// <summary>
    /// 碰撞器进入
    /// </summary>
    /// <param name="_other"></param>
    private void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.gameObject.tag.Equals("Player"))
        {
            ExpressionScore.SInstance.AddScore(AddScore); //加分	
            SoundEffectController.PlaySound(EatGoldClip, transform.position); //播放吃金币声音
            Instantiate(DestroyAnim, transform.position, Quaternion.identity); //生成加分效果
            Destroy(gameObject); //销毁自己
        }
    }
}