using System.Collections.Generic;
using gameBase;
using good;
using UnityEngine;

namespace map
{
    public class GoldMap : BaseMap
    {
        public static GoldMap Instance; //单例模式

        public Gold GoldPrefab; //金币物体

        public List<Sprite> GoldImageList; //金币图片数组
        public List<int> AddScoreList; //添加分数的数组 

        protected override void Awake()
        {
            base.Awake(); //初始化
            Instance = this;
        }

        /// <summary>
        /// 开始的时候
        /// </summary>
        private void Start()
        {
            BrickArr = MapManager.Instance.GetLayerData("gold"); //得到金币数组
            CreateGold();
        }

        /// <summary>
        /// 创建金币
        /// </summary>
        private void CreateGold()
        {
            float startX = -(XGridNum - 0.4f) / 2;

            int endIndex = (int) (XGridNum / 0.4f + 2);

            Generate(0); //初始化一下地图
        }


        /// <summary>
        /// 移动金币
        /// </summary>
        /// <param name="_nowX"></param>
        /// <param name="_speed"></param>
        public override void Remove(float _nowX, float _speed)
        {
            for (int i = 0; i < transform.childCount; i++) //移动金币
            {
                Transform t = transform.GetChild(i);
                t.position = new Vector2(t.position.x - _speed, t.position.y); //重新计算金币的位置
                if (t.position.x < -XGridNum / 2 - 0.4f)
                {
                    Destroy(t.gameObject); //销毁金币
                }
            }

            Generate(_nowX);
        }

        /// <summary>
        /// 生成地图
        /// </summary>
        protected override void Generate(float _nowX)
        {
            float startX = -(XGridNum - 0.4f) / 2 - _nowX;

            int endIndex = (int) ((_nowX + XGridNum) / 0.4f + 2);

            for (int j = NowIndex; j < endIndex && j < BrickArr.GetLength(1); j++) //生成砖块
            {
                for (int i = 0; i < BrickArr.GetLength(0); i++)
                {
                    int goldType = BrickArr[i, j];
                    if (goldType != 0) //不是0创建金币
                    {
                        float y = 4.8f - i * 0.4f; //计算过程  5 - 0.2f
                        Vector2 pos = new Vector2(startX + 0.4f * j, y);

                        Gold gold = Instantiate(GoldPrefab, pos, Quaternion.identity, transform); //生成一个金币

                        gold.SetType(GoldImageList[goldType - 1], AddScoreList[goldType - 1]); //设置金币类型
                    }
                }
            }

            NowIndex = endIndex;
        }
    }
}