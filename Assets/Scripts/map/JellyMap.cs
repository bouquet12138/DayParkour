using UnityEngine;

namespace map
{
    public class JellyMap : BaseMap
    {
        public static JellyMap Instance; //单例模式
        public GameObject JellyPrefab; //果冻预制体

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        public override void Remove(float _nowX, float _speed)
        {
        }

        protected override void Generate(float _nowX)
        {
            throw new System.NotImplementedException();
        }
    }
}