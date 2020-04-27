using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic {

    public static class ListExtension {

        public static T Pop<T>(this List<T> list, int index = 0)
        {
            T t = list[index];
            list.RemoveAt(index);
            return t;
        }

        public static T lastData<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static T PopLast<T>(this List<T> list)
        {
            return list.Pop(list.Count - 1);
        }
    }
}