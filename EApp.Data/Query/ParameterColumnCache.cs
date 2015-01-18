using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Data.Query
{
    internal sealed class ParameterColumnCache
    {
        private readonly static ParameterColumnCache instance = new ParameterColumnCache();

        private List<string> parameterColumnList = new List<string>();

        private ParameterColumnCache() { }

        public static ParameterColumnCache Instance 
        {
            get 
            {
                return instance;
            }
        }

        public string GetParameterColumn(string dbColumn)
        {
            int count = parameterColumnList.Count(c => c.StartsWith(dbColumn));

            string parameterColumn = dbColumn + (count > 0 ? "_" + count.ToString() : string.Empty);

            this.parameterColumnList.Add(parameterColumn);

            return parameterColumn;
        }

        public void Reset() 
        {
            this.parameterColumnList.Clear();
        }
    }
}
