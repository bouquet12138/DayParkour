using System.Collections.Generic;
using gameBase;
using UnityEngine;

namespace map
{
    public class BrickMap : BaseMap
    {
        public static BrickMap Instance; //单例模式

        public List<GameObject> BrickList; //存放砖块的数组

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        /// <summary>
        /// 第一次可见
        /// </summary>
        private void Start()
        {
            BrickArr = MapManager.Instance.GetLayerData("brick"); //得到砖块数组
            CreateBrick();
        }

        /// <summary>
        /// 创建砖块
        /// </summary>
        private void CreateBrick()
        {
            Generate(0);
        }


        /// <summary>
        /// 移动砖块
        /// </summary>
        /// <param name="_nowX"></param>
        /// <param name="_speed"></param>
        public override void Remove(float _nowX, float _speed)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform t = transform.GetChild(i);
                t.position = new Vector2(t.position.x - _speed, t.position.y); //重新计算brick的位置
                if (t.position.x < -XGridNum / 2 - 0.4f)
                {
                    Destroy(t.gameObject); //销毁砖块
                }
            }

            Generate(_nowX); //看一下是否有需要生成的
        }

        protected override void Generate(float _nowX)
        {
            float startX = -(XGridNum - 0.4f) / 2 - _nowX;
            int endIndex = (int) ((_nowX + XGridNum) / 0.4f + 2);


            for (int j = NowIndex; j < endIndex && j < BrickArr.GetLength(1); j++)
            {
                for (int i = 0; i < BrickArr.GetLength(0); i++)
                {
                    int brickIndex = BrickArr[i, j];
                    if (brickIndex != 0) //不是0创建砖块
                    {
                        float y = -3.7f - i * 0.4f; //计算过程 -2.2f - 1.5f
                        Vector2 pos = new Vector2(startX + 0.4f * j, y);

                        if (brickIndex == 1)
                            pos += new Vector2(-0.127f, 0);
                        else if (brickIndex == 5)
                            pos += new Vector2(0.08f, 0);

                        Instantiate(BrickList[brickIndex - 1], pos, Quaternion.identity, transform); //生成一个砖块
                        break;
                    }
                }
            }

            NowIndex = endIndex;
        }
    }
}