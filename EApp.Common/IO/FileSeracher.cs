using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EApp.Common.IO
{
    public enum FileDateFilterTypes
    {
        LastAccessDate,
        CreateDate,
        LastModifiedDate
    }

    public enum FileDateFilterOperators
    {
        AnyDate,
        Is,
        IsBefore,
        IsAfter
    }

    public enum FileSizeFilterOperators
    {
        AnySize,
        Equals,
        LessThan,
        GreaterThan
    }

    public class FileSearchCriterias
    {
        private string directoryPath = string.Empty;

        public FileSearchCriterias(string directoryPath) : this(directoryPath, "*.*") { }

        public FileSearchCriterias(string directoryPath, string searchPatternName)
        {
            if (string.IsNullOrEmpty(directoryPath))
                throw new ArgumentNullException("Path cannot be null or empty.");

            this.directoryPath = directoryPath;
            this.searchPatternName = searchPatternName;
        }

        public string DirectoryPath
        {
            get
            {
                return directoryPath;
            }
        }

        private string searchPatternName = "*.*";
        public string SearchPatternName
        {
            get
            {
                return searchPatternName;
            }
            set
            {
                searchPatternName = value;
            }
        }

        private FileDateFilterTypes fileDateFilterType = FileDateFilterTypes.LastAccessDate;
        public FileDateFilterTypes FileDateFilterType
        {
            get
            {
                return fileDateFilterType;
            }
            set
            {
                fileDateFilterType = value;
            }
        }

        private FileDateFilterOperators dateFilterOperator = FileDateFilterOperators.AnyDate;
        public FileDateFilterOperators DateFilterOperator
        {
            get
            {
                return dateFilterOperator;
            }
            set
            {
                dateFilterOperator = value;
            }
        }

        private DateTime? fileDate = DateTime.Now.Date;
        public DateTime? FileDate
        {
            get
            {
                if (dateFilterOperator == FileDateFilterOperators.AnyDate)
                    return null;

                return fileDate;
            }
            set
            {
                fileDate = value;
            }
        }

        private long? fileSize = 0;
        public long? FileSize
        {
            get
            {
                if (sizeFilterOperator == FileSizeFilterOperators.AnySize)
                    return null;

                return fileSize;
            }
            set
            {
                fileSize = value;
            }
        }

        private FileSizeFilterOperators sizeFilterOperator = FileSizeFilterOperators.AnySize;
        public FileSizeFilterOperators SizeFilterOperator
        {
            get
            {
                return sizeFilterOperator;
            }
            set
            {
                sizeFilterOperator = value;
            }
        }

        private bool hiddenFilesIncluded;
        public bool HiddenFilesIncluded
        {
            get
            {
                return hiddenFilesIncluded;
            }
            set
            {
                hiddenFilesIncluded = value;
            }
        }

        private bool systemFilesIncluded;
        public bool SystemFilesIncluded
        {
            get
            {
                return systemFilesIncluded;
            }
            set
            {
                systemFilesIncluded = value;
            }
        }
    }

    public class FileSeracher
    {
        private FileSearchCriterias searchCriteria;

        public FileSeracher(FileSearchCriterias searchCriteria) 
        {
            this.searchCriteria = searchCriteria;
        }

        public FileInfo[] SearchFiles()
        {
            string[] files = Directory.GetFiles(this.searchCriteria.DirectoryPath, this.searchCriteria.SearchPatternName, SearchOption.AllDirectories);

            if (files == null)
                return null;

            List<FileInfo> fileInfoList = new List<FileInfo>();

            for (int fileIndex = 0; fileIndex < files.Length; fileIndex++)
            {
                FileInfo fileInfo = new FileInfo(files[fileIndex]);

                bool matchQueryCriteria = true;

                if (this.searchCriteria.DateFilterOperator != FileDateFilterOperators.AnyDate)
                {
                    DateTime fileOperationTime;

                    if (this.searchCriteria.FileDateFilterType == FileDateFilterTypes.LastAccessDate)
                        fileOperationTime = fileInfo.LastAccessTimeUtc;
                    else if (searchCriteria.FileDateFilterType == FileDateFilterTypes.CreateDate)
                        fileOperationTime = fileInfo.CreationTimeUtc;
                    else
                        fileOperationTime = fileInfo.LastWriteTimeUtc;

                    int dateCompareResult = fileOperationTime.CompareTo(searchCriteria.FileDate.Value.ToUniversalTime());

                    if (this.searchCriteria.DateFilterOperator == FileDateFilterOperators.Is)
                        matchQueryCriteria = dateCompareResult == 0;
                    else if (this.searchCriteria.DateFilterOperator == FileDateFilterOperators.IsBefore)
                        matchQueryCriteria = dateCompareResult < 0;
                    else if (this.searchCriteria.DateFilterOperator == FileDateFilterOperators.IsAfter)
                        matchQueryCriteria = dateCompareResult > 0;

                    if (!matchQueryCriteria)
                        continue;
                }

                if (this.searchCriteria.SizeFilterOperator != FileSizeFilterOperators.AnySize)
                {
                    long size = fileInfo.Length;
                    int sizeCompareResult = size.CompareTo(searchCriteria.FileSize.Value);

                    if (this.searchCriteria.SizeFilterOperator == FileSizeFilterOperators.Equals)
                        matchQueryCriteria = matchQueryCriteria && (sizeCompareResult == 0);
                    else if (this.searchCriteria.SizeFilterOperator == FileSizeFilterOperators.GreaterThan)
                        matchQueryCriteria = matchQueryCriteria && (sizeCompareResult > 0);
                    else if (this.searchCriteria.SizeFilterOperator == FileSizeFilterOperators.LessThan)
                        matchQueryCriteria = matchQueryCriteria && (sizeCompareResult < 0);

                    if (!matchQueryCriteria)
                        continue;
                }

                if (!this.searchCriteria.HiddenFilesIncluded)
                {
                    matchQueryCriteria = matchQueryCriteria && (fileInfo.Attributes != FileAttributes.Hidden);
                    if (!matchQueryCriteria)
                        continue;
                }

                if (!this.searchCriteria.SystemFilesIncluded)
                    matchQueryCriteria = matchQueryCriteria && (fileInfo.Attributes != FileAttributes.System);

                if (matchQueryCriteria)
                {
                    fileInfoList.Add(fileInfo);
                }
            }

            fileInfoList.Sort(new Comparison<FileInfo>(FileInfoComparison));

            return fileInfoList.ToArray();
        }

        private static int FileInfoComparison(FileInfo x, FileInfo y)
        {
            return x.Directory.Name.CompareTo(y.Directory.Name);
        }
    }
}
