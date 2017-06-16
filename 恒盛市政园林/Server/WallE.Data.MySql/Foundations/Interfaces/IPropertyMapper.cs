// ***********************************************************************
// Assembly         : WallE.dll
// Author           : xx Chen
// Created          : 2015-08-31 13:21
// 
// Last Modified By : 郭华华
// Last Modified On : 2015-08-31 15:21
// ***********************************************************************
// <copyright file="IPropertyMapper.cs" company="深圳筑星科技有限公司">
//      Copyright (c) BStar All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WallE.Data.MySql
{
    public interface IPropertyMapper
    {
        string MapPropertyName(string propertyName);
    }
}