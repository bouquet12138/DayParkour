using System.Collections.Generic;
using gameBase;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public static Hero Instance; //单例模式

    public AudioClip JumpAudio; //跳的声音
    public AudioClip SecondJumpAudio; //二级跳的声音

    public Sprite RunSprite; //奔跑的图片

    private const int RUN = 0; //跑
    private const int GLIDE = 1; //下滑
    private const int JUMP = 2; //跳
    private const int TWO_JUMP = 3; //二级跳
    private const int DROP = 4; //下落

    private Animator mAnimator; //动画控制器
    private Rigidbody2D mRigidBody2D; //刚体组件
    private BoxCollider2D mBoxCollider2D; //碰撞体盒子
    private SpriteRenderer mSpriteRenderer; //渲染器

    private float mStartX; //英雄开始的X

    private int mTwoJumpNum = 1; //二级跳的数量
    private Vector2 mInitBox; //碰撞盒的x y


    /// <summary>
    /// 唤醒的时候
    /// </summary>
    private void Awake()
    {
        Instance = this;
        mAnimator = GetComponent<Animator>(); //动画
        mRigidBody2D = GetComponent<Rigidbody2D>(); //刚体组件
        mBoxCollider2D = GetComponent<BoxCollider2D>(); //盒子碰撞体
        mSpriteRenderer = GetComponent<SpriteRenderer>(); //渲染器

        float heroWidth = GetComponent<Renderer>().bounds.size.x; //hero的宽
        float xGridNum = Screen.width / (Screen.height / 10f);
        mStartX = -(xGridNum - heroWidth) / 2;
        transform.position = new Vector3(mStartX, transform.position.y); //设置hero的y
        mInitBox = mBoxCollider2D.size; //初始一下碰撞盒的大小
    }


    private void FixedUpdate()
    {
        if (mRigidBody2D.velocity.y < -0.1f)
        {
            //Debug.Log("垂直速度" + mRigidBody2D.velocity.y);
            int currentState = GetCurrentState();
            if (currentState == GLIDE)
            {
                if (mRigidBody2D.velocity.y < -0.5f)
                {
                    mBoxCollider2D.size = new Vector2(1, 1f); //设置碰撞体尺寸
                    SetCurrentState(DROP); //将当前状态设为下降
                }
            }
            else
            {
                if (GetCurrentState() != DROP) //如果当前状态不为下落
                {
                    mBoxCollider2D.size = new Vector2(1, 1f); //设置碰撞体尺寸
                    SetCurrentState(DROP); //将当前状态设为下降
                }
            }
        }
    }

    /// <summary>
    /// 碰撞器进入
    /// </summary>
    /// <param name="_other"></param>
    private void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.gameObject.tag.Equals("Ground"))
        {
            int currentState = GetCurrentState();

            if (currentState != RUN && currentState != GLIDE && mRigidBody2D.velocity.y < 0.1f)
            {
                mTwoJumpNum = 1; //又可以二级跳了
                mBoxCollider2D.size = mInitBox; //恢复碰撞体尺寸
                //  mSpriteRenderer.sprite = RunSprite; //设为run图片
                SetCurrentState(RUN); //当前状态跑
            }
        }
    }

    /// <summary>
    /// 得到当前运动状态
    /// </summary>
    /// <returns>当前运动状态</returns>
    private int GetCurrentState()
    {
        return mAnimator.GetInteger("currentState");
    }

    /// <summary>
    /// 设置当前状态
    /// </summary>
    private void SetCurrentState(int _state)
    {
        mAnimator.SetInteger("currentState", _state);
    }

    /// <summary>
    /// 跳的方法
    /// </summary>
    public void Jump()
    {
        if (GetCurrentState() == RUN) //如果当前状态为跑
        {
            SetCurrentState(JUMP);
            SoundEffectController.PlaySound(JumpAudio); //播放音效
            mRigidBody2D.velocity = new Vector2(0, 9f); //来个向上的速度
        }
        else if ((GetCurrentState() == JUMP || GetCurrentState() == DROP) && mTwoJumpNum > 0) //如果不是二级跳
        {
            mTwoJumpNum--; //可以二级跳的数目减减
            SetCurrentState(TWO_JUMP); //设置当前状态为二级跳
            SoundEffectController.PlaySound(SecondJumpAudio); //播放二级跳音效
            mRigidBody2D.velocity = new Vector2(0, Mathf.Max(6f, mRigidBody2D.velocity.y + 4)); //来个向上的速度
        }
    }

    /// <summary>
    /// 下滑
    /// </summary>
    public void Slide()
    {
        if (GetCurrentState() == RUN) //如果当前状态为跑
        {
            SetCurrentState(GLIDE); //下滑
            mBoxCollider2D.size = new Vector2(1, 0.6f); //高度减半
            transform.position = new Vector2(transform.position.x,
                transform.position.y - 0.3f); //向下移动0.3
        }
    }

    /// <summary>
    /// 下滑后复原
    /// </summary>
    public void SlideRestore()
    {
        if (GetCurrentState() == GLIDE) //如果当前状态为下滑
        {
            SetCurrentState(RUN); //跑
            mBoxCollider2D.size = mInitBox; //高度复原
            transform.position = new Vector2(transform.position.x,
                transform.position.y + 0.3f); //向上移动0.3
        }
    }

    /// <summary>
    /// 移动主人公
    /// </summary>
    public void RemoveHero(float _distance)
    {
        transform.position = new Vector2(mStartX + _distance,
            transform.position.y);
    }

    /// <summary>
    /// 检查用户是否在标准位置
    /// 比如 砖块移动用户退后等
    /// </summary>
    /// <param name="_standardX">标准位置</param>
    /// <param name="_speedX">移动回正常位置的速度</param>
    public void CheckHeroPosition(float _standardX, float _speedX)
    {
        if (transform.position.x < mStartX + _standardX)
        {
            transform.position = new Vector2(transform.position.x + _speedX,
                transform.position.y);
        }
    }
}