using System.Threading.Tasks;

namespace WallE.Data.MySql
{
    public interface IRecordIdManager
    {
        /// <summary>
        /// 获取记录Id
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<Result> GenerateIdAsync(string connStr, string tableName);

        /// <summary>
        ///获取多个Id
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="tableName"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        Task<Result> GenerateIdAsync(string connStr, string tableName, int totalCount);
    }
}
