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
                    for (int i = 0; i <= cacheParams.Length - 1; i++)
                    {
                        parms.Add(((ICloneable)cacheParams[i]).Clone());
                        parms[i].Value = values[i];
                        this.db.DBProvider.AdjustParameter(parms[i]);
                    }
                }
                else
                {
                    for (int i = 0; i <= cacheParams.Length - 1; i++)
                    {
                        parms.Add(((ICloneable)cacheParams[i]).Clone());
                        parms[i].DbType = types[i];
                        parms[i].Value = values[i];
                        this.db.DBProvider.AdjustParameter(parms[i]);
                    }
                }
            }

        }


        public void CreateAndCacheParameters(string key, DbCommand cmd, string[] names, DbType[] types, object[] values)
        {
            DbParameterCollection parms = cmd.Parameters;
            parms.Clear();

            if (names != null & names.Length > 0)
            {
                if (types == null)
                {

                    for (int i = 0; i <= names.Length - 1; i++)
                    {
                    }

                }
                else
                {
                }

            }

        }


        //        public void CreateAndCacheParameters(string key, DbCommand cmd, string[] names, DbType[] types, object[] values)
        //{
        //    DbParameterCollection parms = cmd.Parameters;
        //    parms.Clear();
        //    if (names != null && names.Length > 0)
        //    {
        //        if (types == null)
        //        {
        //            for (int i = 0; i < names.Length; i++)
        //            {
        //                db.AddInParameter(cmd, names[i], values[i]);

        //                AdjustParamNameForOracle(cmd, names[i]);
        //            }
        //        }
        //        else
        //        {
        //            for (int i = 0; i < names.Length; i++)
        //            {
        //                db.AddInParameter(cmd, names[i], types[i], values[i]);

        //                AdjustParamNameForOracle(cmd, names[i]);
        //            }
        //        }
        //        cache.Add(key, CreateCachableParamsClone(parms));
        //    }
        //}

        private DbParameter[] CreateCachableParamsClone(DbParameterCollection parms)
        {
            int i = 0;
            DbParameter[] cacheParams = new DbParameter[parms.Count];
            foreach (DbParameter param in parms)
            {
                cacheParams[i] = (DbParameter)(param as ICloneable).Clone();
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

        }


    }

}
