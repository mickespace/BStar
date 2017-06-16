using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallE.Data.MySql;
using WallE.Platform.Models;

namespace WallE.Platform.DalService
{
    internal class CategoryDal
    {
        private static CategoryDal _ins;
        private CategoryDal() { }

        /// <summary>
        /// 全局唯一变量
        /// </summary>
        public static CategoryDal Ins
        {
            get { return _ins ?? (_ins = new CategoryDal()); }
        }

        /// <summary>
        /// 获取分类信息
        /// </summary>
        /// <param name="categoryType">分类类型</param>
        /// <returns></returns>
        public async Task<List<Category>> GetCategorysAsync(int categoryType)
        {
            try
            {
                var dbAccessor = M.DbAccessorMgnt.GetMgntDbAccessor();
                var records = await dbAccessor.QueryAsync(Category.TableName, Category.ColType, categoryType,
                    new Dictionary<string, bool> { { Category.ColName, true } });
                var entities = records.Select(t => t.As<Category>()).ToList();
                return entities;
            }
            catch (Exception ex)
            {
                const string error = "获取分类信息失败！";
                LoggerHelper.Error(error, ex);
                return null;
            }
        }

        /// <summary>
        /// 保存分类信息
        /// </summary>
        /// <param name="categoryType">分类类型</param>
        /// <param name="name">分类名称</param>
        /// <returns></returns>
        public async Task<Category> AddCategoryAsync(int categoryType,string name)
        {
            try
            {
                var category = new Category
                {
                    Id = Guid.NewGuid().ToString().ToLower(),
                    Name = name,
                    Type = categoryType
                };
                var dbAccessor = M.DbAccessorMgnt.GetMgntDbAccessor();
                var result = await dbAccessor.InsertAsync(Category.TableName, new DataRecord(category.ToDictionary()));
                if (result.ResultType==ResultType.Ok)
                    return category;
                LoggerHelper.Error(new Exception("保存分类信息失败，请重试！"));
                return null;
            }
            catch (Exception ex)
            {
                const string error = "保存分类信息失败，请重试！";
                LoggerHelper.Error(error, ex);
                return null;
            }
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id">删除的Id</param>
        public async Task<bool> DeleteCategoryAsync(string id)
        {
            try
            {
                var dbAccessor = M.DbAccessorMgnt.GetMgntDbAccessor();
                var records = await dbAccessor.QueryAsync(Category.TableName, Category.ColId, id);
                var record = records.FirstOrDefault();
                if (record == null)
                {
                    var errorStr = $"删除分类信息失败，不存在编号为（{id}）的分类！";
                    LoggerHelper.Info(errorStr);
                    return false;
                }
                return await dbAccessor.DeleteAsync(Category.TableName, Category.ColId, id);
            }
            catch (Exception ex)
            {
                const string error = "删除分类信息失败，请重试！";
                LoggerHelper.Error(error, ex);
                return false;
            }
        }
    }
}