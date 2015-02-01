using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Util
{
    public static class ArrayUtil
    {
        public static bool IsExistInArray<TObject>(TObject[] objectArray, TObject item)
        {
            if (objectArray == null || item == null)
            {
                return false;
            }

            foreach (TObject itemObject in objectArray)
            {
                if (itemObject.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool EqualsStringArray(string[][] arrayX, string[][] arrayY)
        {
            if (arrayX == null && arrayY == null)
            {
                return true;
            }

            if (arrayX == null && arrayY != null)
            {
                return false;
            }

            if (arrayX != null && arrayY == null)
            {
                return false;
            }

            if (!arrayX.Length.Equals(arrayY.Length))
            {
                return false;
            }

            for (int i = 0; i < arrayX.Length; i++)
            {
                for (int j = 0; j < arrayX[i].Length; j++)
                {
                    if (!arrayX[i][j].Equals(arrayY[i][j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
