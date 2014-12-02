using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Util
{
    public sealed class CommonHelper
    {
        private CommonHelper() { }

        public static bool IsExistInArray<TObject>(TObject[] objectArray, TObject item)
        {
            if (objectArray == null || item == null)
                return false;

            foreach (TObject itemObject in objectArray)
            {
                if (itemObject.Equals(item))
                    return true;
            }

            return false;
        }

        public static bool EqualsStringArray(string[][] array1, string[][] array2)
        {
            if (array1 == null && array2 == null)
                return true;

            if (array1 == null && array2 != null)
                return false;

            if (array1 != null && array2 == null)
                return false;

            if (!array1.Length.Equals(array2.Length))
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                for (int j = 0; j < array1[i].Length; j++)
                {
                    if (!array1[i][j].Equals(array2[i][j]))
                        return false;
                }
            }

            return true;
        }

    }
}
