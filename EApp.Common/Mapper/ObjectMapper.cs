using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper;
using EmitMapper.Mappers;

namespace EApp.Common.Mapper
{
    public sealed class ObjectMapper
    {
        private ObjectMapper() { }

        public static object CopyObject(object sourceObject)
        {
            return null;
        }

        public static TObject CopyObject<TObject>(TObject sourceObject)
        {
            ObjectsMapper<TObject,TObject> mapper = ObjectMapperManager.DefaultInstance.GetMapper<TObject, TObject>();

            return mapper.Map(sourceObject);
        }

        public static bool EqualsObject(object objectX, object objectY)
        {
            return false;
        }

        public static bool EqualsObject<TObject>(TObject objectX, TObject objectY)
        {
            return false;
        }

    }
}
