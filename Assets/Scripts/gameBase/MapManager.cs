using System;
using System.Collections.Generic;
using bean;
using UnityEngine;
using UnityEngine.Analytics;

namespace gameBase
{
    public class MapManager
    {
        private static MapManager sInstance = null; //单例模式

        public static MapManager Instance
        {
            get { return sInstance ?? (sInstance = new MapManager()); }
        }


        private MapManager()
        {
            Debug.Log("MapManager 构造器");
            Analysis(); //解析一下
        }


        private string mMapContent; //地图文件的内容
        private int mWidth, mHeight; //宽 高
        private readonly List<LayerInfo> mLayerInfos = new List<LayerInfo>(); //层信息

        public int Width
        {
            get { return mWidth; }
        }

        public int Height
        {
            get { return mHeight; }
        }


        /// <summary>
        /// 唤醒的时候
        /// </summary>
        private void Analysis()
        {
            mMapContent = Resources.Load<TextAsset>("map1").text; //得到地图内容
            mWidth = GetSize("width", mMapContent); //宽
            mHeight = GetSize("height", mMapContent); //高
            AnalysisData("brick"); //解析一下brick层
            AnalysisData("gold"); //解析一下金币层
        }

        /// <summary>
        /// 得到尺寸
        /// </summary>
        /// <param name="_name">得到何种尺寸如 width height</param>
        /// <param name="_content">内容字符串</param>
        /// <returns></returns>
        private int GetSize(string _name, string _content)
        {
            int startIndex = _content.IndexOf(_name + "=", StringComparison.Ordinal);
            int endIndex = _content.IndexOf("\n", startIndex, StringComparison.Ordinal);

            if (endIndex == -1)
            {
                endIndex = _content.Length;
            }

            startIndex += _name.Length + 1;

            string size = _content.Substring(startIndex, endIndex - startIndex);

            return int.Parse(size); //解析一下字符串
        }

        /// <summary>
        /// 解析该层
        /// </summary>
        /// <param name="_layerName">层名称</param>
        /// <returns></returns>
        private void AnalysisData(string _layerName)
        {
            int startIndex = mMapContent.IndexOf("type=" + _layerName, StringComparison.Ordinal);

            if (startIndex == -1)
                return; //返回不解析了

            int endIndex = mMapContent.IndexOf("[layer]", startIndex, StringComparison.Ordinal); //得到这个层的结尾
            if (endIndex == -1)
                endIndex = mMapContent.Length; //结尾的位置

            string layerContent = mMapContent.Substring(startIndex, endIndex - startIndex); //层的内容

            int width = GetSize("width", layerContent); //宽
            int height = GetSize("height", layerContent); //高


            startIndex = mMapContent.IndexOf("data=", startIndex, StringComparison.Ordinal) + 5;

            String layerData = mMapContent.Substring(startIndex, endIndex - startIndex);

            String[] strArr = layerData.Split(','); //得到字符数组
            int[,] brickArr = new int[height, width]; //初始化一个数组

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int num = int.Parse(strArr[i * width + j].Trim());
                    brickArr[i, j] = num;
                }
            }

            mLayerInfos.Add(new LayerInfo(_layerName, brickArr));

            Debug.Log(new LayerInfo(_layerName, brickArr)); //打印一下
        }

        /// <summary>
        /// 根据层名字得到层数据
        /// </summary>
        /// <param name="_layerName">层名称</param>
        /// <returns></returns>
        public int[,] GetLayerData(string _layerName)
        {
            foreach (var layerInfo in mLayerInfos)
            {
                if (layerInfo.LayerName.Equals(_layerName))
                {
                    return layerInfo.Data;
                }
            }

            return new int[0, 0]; //返回一个空的
        }
    }
}