using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpress.Core.Entities
{
    public class XpressTestingFakeData
    {
        private static List<Group> groups = new List<Group>();

        private static List<string[]> groupDataSource = new List<string[]>();

        private static List<JobType> jobTypes = new List<JobType>();

        private static List<string[]> jobTypeDataSource = new List<string[]>();

        static XpressTestingFakeData() 
        {
            GenerateGroupTestingData();

            GenerateJobTypeTestingData();
        }

        public static List<Group> Groups 
        {
            get 
            {
                return groups;
            }
        }

        public static string[][] GroupDataSource
        {
            get;
            set;
        }

        public static List<JobType> JobTypes 
        {
            get
            {
                return jobTypes;
            }
        }

        public static string[][] JobTypeDataSource
        {
            get;
            set;
        }

        private static void GenerateGroupTestingData() 
        {
            for (int i = 0; i < 10; i++)
            {
                Group group = new Group();
                group.Id = 1000 + i;
                group.Name = "My Group " + (i + 1).ToString();

                IList<Subgroup> subgroupList = new List<Subgroup>();

                for (int j = 0; j < 10; j++)
                {
                    Subgroup subgroup = new Subgroup();
                    subgroup.Id = 1000 + j;
                    subgroup.Name = "[" + group.Name + "] - Subgroup " + (j + 1).ToString();

                    subgroupList.Add(subgroup);
                }

                group.Subgroups = subgroupList;

                groupDataSource.Add(new string[] { group.Id.ToString(), group.Name });

                groups.Add(group);
            }

            GroupDataSource = groupDataSource.ToArray();
        }

        private static void GenerateJobTypeTestingData()
        {
            decimal utilRate = 0.1M;
            decimal baseAmount = 1000;

            for (int i = 0; i < 10; i++)
            {
                JobType jobType = new JobType();
                jobType.Id = 1000 + i;
                jobType.Code = "Job Code " + (i + 1).ToString();
                jobType.UtilRate = utilRate;
                jobType.UtilRate = baseAmount;

                utilRate++;
                baseAmount += 1000;

                jobTypeDataSource.Add(new string[] { jobType.Id.ToString(), jobType.Code });

                jobTypes.Add(jobType);
            }

            JobTypeDataSource = jobTypeDataSource.ToArray();
        }


    }
}
