using System;

namespace WallE.Data.MySql
{
    public interface IDataAccessorMgnt
    {
        [Obsolete("请使用GetDbAccessorByProjectItemKey或GetMgntDbAccess方法", false)]
        IDataAccessor GetDbAccessor(string connStr);

        IDataAccessor GetDbAccessorByProjectItemKey(string projectItemKey);

        IDataAccessor GetMgntDbAccessor();
    }
}
