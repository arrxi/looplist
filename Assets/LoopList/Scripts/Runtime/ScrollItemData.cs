/// <summary>
/// 该类用于无限循环列表传递数据的基类
/// 可以自行扩展
/// </summary>
using System;

namespace ScrollR
{
    [Serializable]
    public class ScrollItemData
    {
        ///
        public int id;

        public ScrollItemData(int id)
        {
            this.id = id;
        }
    }
}