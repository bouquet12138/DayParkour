using System;
using System.Text;

namespace bean
{
    public class LayerInfo
    {
        private readonly String mLayerName; //层的名字
        private readonly int[,] mData; //层的数据


        public LayerInfo(String _layerName, int[,] _data)
        {
            mLayerName = _layerName;
            mData = _data;
        }

        public string LayerName
        {
            get { return mLayerName; }
        }

        public int[,] Data
        {
            get { return mData; }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("layerName:");
            stringBuilder.Append(mLayerName);
            stringBuilder.Append(mData);
            return stringBuilder.ToString();
        }
    }
}