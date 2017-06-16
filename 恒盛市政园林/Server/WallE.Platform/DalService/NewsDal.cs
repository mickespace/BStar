using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallE.Data.MySql;
using WallE.Platform.Models;

namespace WallE.Platform.DalService
{
    /// <summary>
    /// Class NewsionMgnt.
    /// </summary>
    internal class NewsDal
    {
        private static NewsDal _ins;

        private NewsDal()
        {
        }

        /// <summary>
        /// 全局唯一变量
        /// </summary>
        public static NewsDal Ins
        {
            get { return _ins ?? (_ins = new NewsDal()); }
        }

        /// <summary>
        /// 获取信息列表
        /// </summary>
        /// <param name="searchParams">搜索信息</param>
        /// <param name="displayType">显示类型</param>
        public async Task<List<NewsModel>> GetNewsListAsync(SearchParams searchParams, int displayType = -1)
        {
            try
            {
                var searchIndex = searchParams.PageIndex - 1;
                var dbAccessor = M.DbAccessorMgnt.GetMgntDbAccessor();
                var records = new List<DataRecord>();
                var queryDic = new Dictionary<string, object> { { News.ColStatus, 1 } };
                if (!string.IsNullOrEmpty(searchParams.Category))
                    queryDic[News.ColType] = searchParams.Category;
                var srcRecords = await dbAccessor.QueryAsync(News.TableName, queryDic,
                    new Dictionary<string, bool> { { News.ColDisplayOrder, true } });
                if (displayType == -1)
                {
                    records = srcRecords;
                }
                else
                {
                    foreach (var srcRecord in srcRecords)
                    {
                        var curType = (DisplayType)(int)srcRecord[News.ColDisplayType];
                        if (displayType == 0 && (curType & DisplayType.ListPage) != 0)
                            records.Add(srcRecord);
                        if (displayType == 1 && (curType & DisplayType.FrontPage) != 0)
                            records.Add(srcRecord);
                        if (displayType == 2 && (curType & DisplayType.SecondPage) != 0)
                            records.Add(srcRecord);
                    }
                }
                searchParams.TotalCount = records.Count;
                if (!(searchIndex == 0 && searchParams.PageSize < 1))
                    records = records.Skip(searchParams.PageSize * searchIndex).Take(searchParams.PageSize).ToList();
                var entities = records.Select(t => t.As<NewsModel>()).ToList();
                return entities;
            }
            catch (Exception ex)
            {
                const string error = "获取新闻信息列表失败！";
                LoggerHelper.Error(error, ex);
                return new List<NewsModel>();
            }
        }

        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<News> GetNewsInfoAsync(string id)
        {
            try
            {
                var dbAccessor = M.DbAccessorMgnt.GetMgntDbAccessor();
                var records = await dbAccessor.QueryAsync(News.TableName, News.ColId, id);
                var record = records.FirstOrDefault(t => (int)t[News.ColStatus] != -1);
                if (record == null)
                {
                    LoggerHelper.Info($"获取编号为（{id}）新闻信息失败");
                    return null;
                }
                var entity = record.As<News>();
                return entity;
            }
            catch (Exception ex)
            {
                string error = $"获取编号为（{id}）新闻信息失败";
                LoggerHelper.Error(error, ex);
                return null;
            }
        }

        /// <summary>
        /// 上线与下线
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="status">状态(true下线false上线)</param>
        public async Task<ApiResult> ModifyStatusAsync(string id, bool status)
        {
            try
            {
                var dbAccessor = M.DbAccessorMgnt.GetMgntDbAccessor();
                var records = await dbAccessor.QueryAsync(News.TableName, News.ColId, id);
                var record = records.FirstOrDefault(t => (int)t[News.ColStatus] != -1);
                if (record == null)
                {
                    var errorStr = $"设置新闻状态失败，不存在编号为（{id}）的新闻！";
                    LoggerHelper.Info(errorStr);
                    return new ApiResult(new Exception(errorStr));
                }
                var newsion = record.As<News>();
                newsion.Status = status ? 1 : 0;
                var r = await dbAccessor.UpdatetAsync(News.TableName, new DataRecord(newsion.ToDictionary()));
                if (r.ResultType != ResultType.Ok)
                {
                    var errorStr = "设置新闻状态失败，请重试！";
                    LoggerHelper.Error(errorStr, r.Error);
                    return new ApiResult(new Exception(errorStr));
                }
                return new ApiResult();
            }
            catch (Exception ex)
            {
                const string error = "设置新闻状态失败，请重试！";
                LoggerHelper.Error(error, ex);
                return new ApiResult(new Exception(error));
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="updateData">修改的信息</param>
        public async Task<bool> ModifyNewsAsync(string id, Dictionary<string, object> updateData)
        {
            var errorStr = "修改新闻信息失败，请重试！";
            try
            {
                var dbAccessor = M.DbAccessorMgnt.GetMgntDbAccessor();
                var records = await dbAccessor.QueryAsync(News.TableName, News.ColId, id);
                var record = records.FirstOrDefault(t => (int)t[News.ColStatus] != -1);
                if (record == null)
                {
                    errorStr = $"修改新闻失败，不存在编号为（{id}）的新闻！";
                    LoggerHelper.Info(errorStr);
                    return false;
                }
                var news = record.As<News>();
                var dataDic = news.ToDictionary();
                foreach (var key in dataDic.Keys.ToList())
                {
                    if (updateData.ContainsKey(key))
                        dataDic[key] = updateData[key];
                }
                var r = await dbAccessor.UpdatetAsync(News.TableName, new DataRecord(dataDic));
                if (r.ResultType != ResultType.Ok)
                {
                    LoggerHelper.Error(errorStr, r.Error);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LoggerHelper.Error(errorStr, ex);
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">删除的Id</param>
        public async Task<ApiResult> DeleteNewsAsync(string id)
        {
            try
            {
                var dbAccessor = M.DbAccessorMgnt.GetMgntDbAccessor();
                var records = await dbAccessor.QueryAsync(News.TableName, News.ColId, id);
                var record = records.FirstOrDefault(t => (int)t[News.ColStatus] != -1);
                if (record == null)
                {
                    var errorStr = $"删除新闻失败，不存在编号为（{id}）的新闻！";
                    LoggerHelper.Info(errorStr);
                    return new ApiResult(new Exception(errorStr));
                }
                var newsion = record.As<News>();
                newsion.Status = -1;
                var r = await dbAccessor.UpdatetAsync(News.TableName, new DataRecord(newsion.ToDictionary()));
                if (r.ResultType != ResultType.Ok)
                {
                    var errorStr = "删除新闻失败，请重试！";
                    LoggerHelper.Error(errorStr, r.Error);
                    return new ApiResult(new Exception(errorStr));
                }
                return new ApiResult();
            }
            catch (Exception ex)
            {
                const string error = "删除新闻失败，请重试！";
                LoggerHelper.Error(error, ex);
                return new ApiResult(new Exception(error));
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">新添加的信息</param>
        public async Task<Result> CreateNewsAsync(News model)
        {
            try
            {
                var dbAccessor = M.DbAccessorMgnt.GetMgntDbAccessor();
                return await dbAccessor.InsertAsync(News.TableName, new DataRecord(model.ToDictionary()));
            }
            catch (Exception ex)
            {
                const string error = "添加新闻失败，请重试！";
                LoggerHelper.Error(error, ex);
                return new Result(ex);
            }
        }
    }
}