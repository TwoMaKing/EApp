using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;


namespace EApp.Common.DataAccess
{

    /// <summary>
    /// The db param cache.
    /// </summary>
    /// <remarks></remarks>
    internal class DbParameterCache
    {
        private Database db;
     
        public DbParameterCache(Database db)
        {
            this.db = db;
        }

        private Dictionary<string, DbParameter[]> cache = new Dictionary<string, DbParameter[]>();

        public bool IsCache(string key)
        {
            return cache.ContainsKey(key);
        }

        ///<summary>
        ///Adds the parameters from cache.
        ///</summary>
        ///<param name="key">The key.</param>
        ///<param name="cmd">The CMD.</param>
        ///<param name="types">The types.</param>
        ///<param name="values">The values.</param>
        public void AddParametersFromCache(string key, DbCommand cmd, DbType[] types, object[] values)
        {
            DbParameterCollection parms = cmd.Parameters;
            parms.Clear();

            DbParameter[] cacheParams = cache[key];
            if (cacheParams != null & cacheParams.Length > 0)
            {
                if (types == null)
                {
                    for (int i = 0; i < cacheParams.Length; i++)
                    {
                        DbParameter param = (DbParameter)((ICloneable)cacheParams[i]).Clone();
                        param.Value = values[i];
                        this.db.DBProvider.AdjustParameter(param);
                        parms.Add(param);
                    }
                }
                else
                {
                    for (int i = 0; i <= cacheParams.Length - 1; i++)
                    {
                        DbParameter param = (DbParameter)((ICloneable)cacheParams[i]).Clone();
                        param.DbType = types[i];
                        param.Value = values[i];
                        this.db.DBProvider.AdjustParameter(param);
                        parms.Add(param);
                    }
                }
            }
        }


        public void CreateAndCacheParameters(string key, DbCommand cmd, string[] names, DbType[] types, object[] values)
        {
            DbParameterCollection parameters = cmd.Parameters;
            parameters.Clear();

            if (names != null & names.Length > 0)
            {
                if (types == null)
                {
                    for (int i = 0; i < names.Length; i++)
                    {
                        this.db.AddInParameter(cmd, names[i], values[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < names.Length; i++)
                    {
                        this.db.AddInParameter(cmd,names[i],types[i],values[i]);
                    }
                }

                cache.Add(key, CreateCachableParamsClone(parameters));
            }
        }

        private DbParameter[] CreateCachableParamsClone(DbParameterCollection parms)
        {
            int i = 0;
            DbParameter[] cacheParams = new DbParameter[parms.Count];
            foreach (DbParameter param in parms)
            {
                cacheParams[i] = (DbParameter)((ICloneable)param).Clone();
                cacheParams[i].Value = null;
                i += 1;
            }
            return cacheParams;
        }

        private static void AdjustParamNameForOracle(DbCommand cmd, string paramName)
        {
            //For oracle, be careful to avoid paramNames having same ?XXX prefix as param name.
            if (paramName[0].ToString() == "?")
            {
                cmd.CommandText = cmd.CommandText.Replace(paramName, "?");
            }

            if (paramName[0] == ':' && paramName.Length > 25)
            {
                string truncatedParamName = paramName.Substring(0, 15) + paramName.Substring(paramName.Length - 11, 10);
                cmd.Parameters[paramName].ParameterName = truncatedParamName;
                cmd.CommandText = cmd.CommandText.Replace(paramName, truncatedParamName);
            }
        }
    }

}
