// ***********************************************************************
// Assembly         : WallE.Core
// Author           : xxchen
// Created          : 11-25-2015
//
// Last Modified By : xxchen
// Last Modified On : 11-23-2015
// ***********************************************************************
// <copyright file="EntityBase.cs" company="深圳筑星科技有限公司">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WallE.Data.MySql
{
    public abstract class EntityBase : IPropertyMapper
    {
        private object _tag;
        public abstract string MapPropertyName(string propertyName);

        public virtual bool IsValid()
        {
            return true;
        }

        public object GetTag()
        {
            return _tag;
        }

        public void SetTag(object tag)
        {
            _tag = tag;
        }
    }
}