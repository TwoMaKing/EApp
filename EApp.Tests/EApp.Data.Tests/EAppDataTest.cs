using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using EApp.Common;
using EApp.Common.Serialization;
using EApp.Common.Util;
using EApp.Core;
using EApp.Data;
using EApp.Data.Mapping;
using EApp.Data.Queries;
using NUnit.Framework;
using Xpress.Chat.DataObjects;
using Xpress.Chat.Domain.Models;

namespace EApp.Tests
{
    [TestFixture()]
    public class EAppDataTest : TestBase
    {
        public EAppDataTest() : base() { }

        [Test()]
        public void BuildEntityMappingConfigurationXml() 
        {
            EntityMappingConfiguration config = new EntityMappingConfiguration();

            EntityConfiguration entity = new EntityConfiguration();

            entity.Name = "Post";
            entity.TableName = "post";
            entity.TypeName = typeof(Post).AssemblyQualifiedName;

            PropertyConfiguration postIdProperty = new PropertyConfiguration();
            postIdProperty.Name = "Id";
            postIdProperty.PropertyType = typeof(int).FullName;
            postIdProperty.ColumnName = "post_id";
            postIdProperty.IsPrimaryKey = true;
            postIdProperty.IsAutoIdentity = true;
            postIdProperty.IsRelationKey = false;
            postIdProperty.RelatedForeignKey = string.Empty;
            postIdProperty.RelatedType = string.Empty;

            PropertyConfiguration postTopicIdProperty = new PropertyConfiguration();
            postTopicIdProperty.Name = "TopicId";
            postTopicIdProperty.PropertyType = typeof(int).FullName;
            postTopicIdProperty.ColumnName = "post_topic_id";
            postTopicIdProperty.IsPrimaryKey = false;
            postTopicIdProperty.IsAutoIdentity = false;
            postTopicIdProperty.IsRelationKey = true;
            postTopicIdProperty.RelatedForeignKey = "topic_id";
            postTopicIdProperty.RelatedType = typeof(Topic).FullName;

            PropertyConfiguration postAuthorIdProperty = new PropertyConfiguration();
            postAuthorIdProperty.Name = "AuthorId";
            postAuthorIdProperty.PropertyType = typeof(int).FullName;
            postAuthorIdProperty.ColumnName = "post_author_id";
            postAuthorIdProperty.IsPrimaryKey = false;
            postAuthorIdProperty.IsAutoIdentity = false;
            postAuthorIdProperty.IsRelationKey = true;
            postAuthorIdProperty.RelatedForeignKey = "user_id";
            postAuthorIdProperty.RelatedType = typeof(User).FullName;

            PropertyConfiguration postContentProperty = new PropertyConfiguration();
            postContentProperty.Name = "Content";
            postContentProperty.PropertyType = typeof(string).FullName;
            postContentProperty.SqlType = "nvarchar(max)";
            postContentProperty.ColumnName = "post_content";
            postContentProperty.IsPrimaryKey = false;
            postContentProperty.IsAutoIdentity = false;

            PropertyConfiguration postCreationDateTimeProperty = new PropertyConfiguration();
            postCreationDateTimeProperty.Name = "CreationDateTime";
            postCreationDateTimeProperty.PropertyType = typeof(DateTime).FullName;
            postCreationDateTimeProperty.ColumnName = "post_creation_datetime";
            postCreationDateTimeProperty.IsPrimaryKey = false;
            postCreationDateTimeProperty.IsAutoIdentity = false;

            entity.Properties = new PropertyConfiguration[] { postIdProperty, postTopicIdProperty, postAuthorIdProperty,  postContentProperty, postCreationDateTimeProperty };

            config.Entities = new EntityConfiguration[] { entity };

            bool serialized = SerializationManager.Serialize(config, AppDomain.CurrentDomain.BaseDirectory + "EntityMappingConfig.xml");

            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "EntityMappingConfig.xml", FileMode.Open, FileAccess.Read))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] buffers = new byte[2048];

                    int position = fs.Read(buffers, 0, buffers.Length);

                    ms.Seek(0, SeekOrigin.Begin);
                    while (position > 0)
                    {
                        ms.Write(buffers, 0, position);
                        
                        position = fs.Read(buffers, 0, buffers.Length);
                    }
                    ms.Flush();

                    EntityMappingConfiguration entityMappingConfig =
                        ObjectSerializerFactory.GetObjectSerializer("XML").Deserialize<EntityMappingConfiguration>(ms.ToArray());

                    Assert.AreEqual(config.Entities[0].Properties[0].SqlType, entityMappingConfig.Entities[0].Properties[0].SqlType);

                    Assert.AreEqual(config.Entities[0].Properties[2].PropertyType, entityMappingConfig.Entities[0].Properties[2].PropertyType);
                  
                }
            }
           
        }

        [Test()]
        public void Test_LambdaBuilderWhereClause_QueryValueWithoutFieldAndPropertyNoCallMethod()
        {
            Database db = new Database(EApp.Data.DbProviderFactory.Default);

            WhereClauseBuilder<Post> builder = db.DBProvider.CreateWhereClauseBuilder<Post>();

            WhereClauseBuildResult result = builder.BuildWhereClause(p => p.TopicId == 1000 && p.Content == "足球");

            string orderBy = builder.BuildOrderByClause(p => p.CreationDateTime);

            Assert.AreEqual(true, !string.IsNullOrEmpty(result.WhereClause));

            Assert.AreEqual(1000, result.ParameterValues.Values.ToList()[0]);

            Assert.AreEqual("足球", result.ParameterValues.Values.ToList()[1]);
        }

        [Test()]
        public void Test_LambdaBuilderWhereClause_QueryValueWithFieldNoCallMethod()
        {
            Database db = new Database(EApp.Data.DbProviderFactory.Default);

            WhereClauseBuilder<Post> builder = db.DBProvider.CreateWhereClauseBuilder<Post>();

            int topicId = 1000;
            string content = "足球";
            DateTime datetime = DateTimeUtils.ToDateTime("2015-1-22").Value;

            WhereClauseBuildResult result = builder.BuildWhereClause(p => p.TopicId == topicId && 
                                                                          p.Content == content &&
                                                                          p.CreationDateTime < datetime);

            string orderBy = builder.BuildOrderByClause(p => p.CreationDateTime);

            Assert.AreEqual(true, !string.IsNullOrEmpty(result.WhereClause));

            Assert.AreEqual(1000, result.ParameterValues.Values.ToList()[0]);

            Assert.AreEqual("足球", result.ParameterValues.Values.ToList()[1]);

            Assert.AreEqual(datetime, result.ParameterValues.Values.ToList()[2]);
        }

        [Test()]
        public void Test_LambdaBuilderWhereClause_QueryValueWithPropertyNoCallMethod()
        {
            Database db = new Database(EApp.Data.DbProviderFactory.Default);

            WhereClauseBuilder<Post> builder = db.DBProvider.CreateWhereClauseBuilder<Post>();

            PostQueryRequest request = new PostQueryRequest();
            request.TopicId = 1000;
            request.Content = "足球";
            request.CreationDateTimeParam.CreationDateTime = DateTimeUtils.ToDateTime("2015-1-22").Value;

            WhereClauseBuildResult result = builder.BuildWhereClause(p => p.TopicId == request.TopicId &&
                                                                          p.Content == request.Content &&
                                                                          p.CreationDateTime < request.CreationDateTimeParam.CreationDateTime);

            string orderBy = builder.BuildOrderByClause(p => p.CreationDateTime);

            Assert.AreEqual(true, !string.IsNullOrEmpty(result.WhereClause));

            Assert.AreEqual(1000, result.ParameterValues.Values.ToList()[0]);

            Assert.AreEqual("足球", result.ParameterValues.Values.ToList()[1]);

            Assert.AreEqual(request.CreationDateTimeParam.CreationDateTime, result.ParameterValues.Values.ToList()[2]);
        }


        [Test()]
        public void Test_LambdaBuilderWhereClause_QueryValueWithoutFieldAndPropertyWithCallMethod()
        {
            Database db = new Database(EApp.Data.DbProviderFactory.Default);

            WhereClauseBuilder<Post> builder = db.DBProvider.CreateWhereClauseBuilder<Post>();

            DateTime datetime = DateTimeUtils.ToDateTime("2015-1-22").Value;

            WhereClauseBuildResult result = builder.BuildWhereClause(p => p.TopicId.Equals(1000) &&
                                                                          p.Content.Contains("足球") &&
                                                                          p.CreationDateTime < datetime);

            string orderBy = builder.BuildOrderByClause(p => p.CreationDateTime);

            Assert.AreEqual(true, !string.IsNullOrEmpty(result.WhereClause));

            Assert.AreEqual(1000, result.ParameterValues.Values.ToList()[0]);

            Assert.AreEqual("%足球%", result.ParameterValues.Values.ToList()[1]);

            Assert.AreEqual(datetime, result.ParameterValues.Values.ToList()[2]);
        }

        [Test()]
        public void Test_LambdaBuilderWhereClause_QueryValueWithFieldCallMethod()
        {
            Database db = new Database(EApp.Data.DbProviderFactory.Default);

            WhereClauseBuilder<Post> builder = db.DBProvider.CreateWhereClauseBuilder<Post>();

            int topicId = 1000;
            string content = "足球";
            DateTime datetime = DateTimeUtils.ToDateTime("2015-1-22").Value;

            WhereClauseBuildResult result = builder.BuildWhereClause(p => p.TopicId.Equals(topicId) &&
                                                                          p.Content.Contains(content) &&
                                                                          p.CreationDateTime < datetime);

            string orderBy = builder.BuildOrderByClause(p => p.CreationDateTime);

            Assert.AreEqual(true, !string.IsNullOrEmpty(result.WhereClause));

            Assert.AreEqual(1000, result.ParameterValues.Values.ToList()[0]);

            Assert.AreEqual("%足球%", result.ParameterValues.Values.ToList()[1]);

            Assert.AreEqual(datetime, result.ParameterValues.Values.ToList()[2]);
        }


        [Test()]
        public void Test_LambdaBuilderWhereClause_QueryValueWithPropertyWithCallMethod()
        {
            Database db = new Database(EApp.Data.DbProviderFactory.Default);

            WhereClauseBuilder<Post> builder = db.DBProvider.CreateWhereClauseBuilder<Post>();

            PostQueryRequest request = new PostQueryRequest();
            request.TopicId = 1000;
            request.Content = "足球";
            request.CreationDateTimeParam.CreationDateTime = DateTimeUtils.ToDateTime("2015-1-22").Value;

            List<int> topicIds = new List<int>();
            topicIds.Add(1000);
            topicIds.Add(2000);
            topicIds.Add(3000);

            WhereClauseBuildResult result = builder.BuildWhereClause(p => p.TopicId.Equals(request.TopicId) &&
                                                                          p.Content.Contains(request.Content) &&
                                                                          p.CreationDateTime < request.CreationDateTimeParam.CreationDateTime);

            string orderBy = builder.BuildOrderByClause(p => p.CreationDateTime);

            Assert.AreEqual(true, !string.IsNullOrEmpty(result.WhereClause));

            Assert.AreEqual(1000, result.ParameterValues.Values.ToList()[0]);

            Assert.AreEqual("%足球%", result.ParameterValues.Values.ToList()[1]);

            Assert.AreEqual(request.CreationDateTimeParam.CreationDateTime, result.ParameterValues.Values.ToList()[2]);
        }

        [Test()]
        public void Test_LambdaBuilderWhereClause_CombineCondition_QueryValueWithoutFieldAndPropertyNoCallMethod()
        {
            Database db = new Database(EApp.Data.DbProviderFactory.Default);

            WhereClauseBuilder<Post> builder = db.DBProvider.CreateWhereClauseBuilder<Post>();

            WhereClauseBuildResult result = builder.BuildWhereClause(p => (p.TopicId < 1000 || p.TopicId > 2000) && 
                                                                          (p.Content == "足球" || p.Content == "篮球"));

            string orderBy = builder.BuildOrderByClause(p => p.CreationDateTime);

            Assert.AreEqual(true, !string.IsNullOrEmpty(result.WhereClause));

            Assert.AreEqual(1000, result.ParameterValues.Values.ToList()[0]);

            Assert.AreEqual(2000, result.ParameterValues.Values.ToList()[1]);

            Assert.AreEqual("足球", result.ParameterValues.Values.ToList()[2]);

            Assert.AreEqual("篮球", result.ParameterValues.Values.ToList()[3]);
        }

        [Test()]
        public void Test_LambdaBuilderWhereClause_CombineCondition_QueryValueWithFieldNoCallMethod()
        {
            Database db = new Database(EApp.Data.DbProviderFactory.Default);

            WhereClauseBuilder<Post> builder = db.DBProvider.CreateWhereClauseBuilder<Post>();

            int topicId = 1000;
            int topicId1 = 2000;
            string content = "足球";
            string content1 = "篮球";
            DateTime datetime = DateTimeUtils.ToDateTime("2015-1-22").Value;

            WhereClauseBuildResult result = builder.BuildWhereClause(p => (p.TopicId < topicId || p.TopicId > topicId1) &&
                                                                          (p.Content == content || p.Content == content1) ||
                                                                           p.CreationDateTime < datetime);

            string orderBy = builder.BuildOrderByClause(p => p.CreationDateTime);

            Assert.AreEqual(true, !string.IsNullOrEmpty(result.WhereClause));

            Assert.AreEqual(1000, result.ParameterValues.Values.ToList()[0]);

            Assert.AreEqual(2000, result.ParameterValues.Values.ToList()[1]);

            Assert.AreEqual("足球", result.ParameterValues.Values.ToList()[2]);

            Assert.AreEqual("篮球", result.ParameterValues.Values.ToList()[3]);

            Assert.AreEqual(datetime, result.ParameterValues.Values.ToList()[4]);
        }

        [Test()]
        public void Test_LambdaBuilderWhereClause_CombineCondition_QueryValueWithPropertyNoCallMethod()
        {
            Database db = new Database(EApp.Data.DbProviderFactory.Default);

            WhereClauseBuilder<Post> builder = db.DBProvider.CreateWhereClauseBuilder<Post>();

            PostQueryRequest request = new PostQueryRequest();
            request.TopicId = 1000;
            request.MiscParams.TopicId = 2000;
            request.Content = "足球";
            request.MiscParams.Content = "篮球";
            request.CreationDateTimeParam.CreationDateTime = DateTimeUtils.ToDateTime("2015-1-22").Value;

            WhereClauseBuildResult result = builder.BuildWhereClause(p => (p.TopicId < request.TopicId || p.TopicId > request.MiscParams.TopicId) &&
                                                                          (p.Content == request.Content || p.Content == request.MiscParams.Content) ||
                                                                           p.CreationDateTime < request.CreationDateTimeParam.CreationDateTime);

            string orderBy = builder.BuildOrderByClause(p => p.CreationDateTime);

            Assert.AreEqual(true, !string.IsNullOrEmpty(result.WhereClause));

            Assert.AreEqual(1000, result.ParameterValues.Values.ToList()[0]);

            Assert.AreEqual(2000, result.ParameterValues.Values.ToList()[1]);

            Assert.AreEqual("足球", result.ParameterValues.Values.ToList()[2]);

            Assert.AreEqual("篮球", result.ParameterValues.Values.ToList()[3]);

            Assert.AreEqual(request.CreationDateTimeParam.CreationDateTime, result.ParameterValues.Values.ToList()[4]);

        }

        [Test()]
        public void Test_LambdaBuilderWhereClause_CombineCondition_QueryValueWithoutFieldAndPropertyWithCallMethod()
        {
            Database db = new Database(EApp.Data.DbProviderFactory.Default);

            WhereClauseBuilder<Post> builder = db.DBProvider.CreateWhereClauseBuilder<Post>();

            WhereClauseBuildResult result = builder.BuildWhereClause(p => (p.TopicId < 1000 || p.TopicId > 2000) &&
                                                                          (p.Content.Contains("足球") || p.Content.Contains("篮球")));

            string orderBy = builder.BuildOrderByClause(p => p.CreationDateTime);

            Assert.AreEqual(true, !string.IsNullOrEmpty(result.WhereClause));

            Assert.AreEqual(1000, result.ParameterValues.Values.ToList()[0]);

            Assert.AreEqual(2000, result.ParameterValues.Values.ToList()[1]);

            Assert.AreEqual("%足球%", result.ParameterValues.Values.ToList()[2]);

            Assert.AreEqual("%篮球%", result.ParameterValues.Values.ToList()[3]);
        }

        [Test()]
        public void Test_LambdaBuilderWhereClause_CombineCondition_QueryValueWithFieldWithCallMethod()
        {
            Database db = new Database(EApp.Data.DbProviderFactory.Default);

            WhereClauseBuilder<Post> builder = db.DBProvider.CreateWhereClauseBuilder<Post>();

            int topicId = 1000;
            int topicId1 = 2000;
            string content = "足球";
            string content1 = "篮球";
            DateTime datetime = DateTimeUtils.ToDateTime("2015-1-22").Value;

            WhereClauseBuildResult result = builder.BuildWhereClause(p => (p.TopicId < topicId || p.TopicId > topicId1) &&
                                                                          (p.Content.Contains(content) || p.Content.Contains(content1)) ||
                                                                           p.CreationDateTime < datetime);

            string orderBy = builder.BuildOrderByClause(p => p.CreationDateTime);

            Assert.AreEqual(true, !string.IsNullOrEmpty(result.WhereClause));

            Assert.AreEqual(1000, result.ParameterValues.Values.ToList()[0]);

            Assert.AreEqual(2000, result.ParameterValues.Values.ToList()[1]);

            Assert.AreEqual("%足球%", result.ParameterValues.Values.ToList()[2]);

            Assert.AreEqual("%篮球%", result.ParameterValues.Values.ToList()[3]);

            Assert.AreEqual(datetime, result.ParameterValues.Values.ToList()[4]);

        }

        [Test()]
        public void Test_LambdaBuilderWhereClause_CombineCondition_QueryValueWithPropertyWithCallMethod() 
        {
            Database db = new Database(EApp.Data.DbProviderFactory.Default);

            WhereClauseBuilder<Post> builder = db.DBProvider.CreateWhereClauseBuilder<Post>();

            PostQueryRequest request = new PostQueryRequest();
            request.TopicId = 1000;
            request.MiscParams.TopicId = 2000;
            request.Content = "足球";
            request.MiscParams.Content = "篮球";
            request.CreationDateTimeParam.CreationDateTime = DateTimeUtils.ToDateTime("2015-1-22").Value;

            WhereClauseBuildResult result = builder.BuildWhereClause(p => (p.TopicId < request.TopicId || p.TopicId > request.MiscParams.TopicId) &&
                                                                          (p.Content.Contains(request.Content) || p.Content.Contains(request.MiscParams.Content)) ||
                                                                          (p.CreationDateTime < request.CreationDateTimeParam.CreationDateTime ||
                                                                           p.CreationDateTime.Equals(request.CreationDateTimeParam.CreationDateTime)));

            string orderBy = builder.BuildOrderByClause(p => p.CreationDateTime);

            Assert.AreEqual(true, !string.IsNullOrEmpty(result.WhereClause));

            Assert.AreEqual(1000, result.ParameterValues.Values.ToList()[0]);

            Assert.AreEqual(2000, result.ParameterValues.Values.ToList()[1]);

            Assert.AreEqual("%足球%", result.ParameterValues.Values.ToList()[2]);

            Assert.AreEqual("%篮球%", result.ParameterValues.Values.ToList()[3]);

            Assert.AreEqual(request.CreationDateTimeParam.CreationDateTime, result.ParameterValues.Values.ToList()[4]);

            Assert.AreEqual(request.CreationDateTimeParam.CreationDateTime, result.ParameterValues.Values.ToList()[5]);
        }
    }
}
