namespace WallE.Data.MySql
{
    public class M
    {
        private static M _m;

        private M()
        {
        }

        public static void Init()
        {
            _m=new M();
            _m._accessorMgnt = new DbAccessorMgnt();
            _m._logManager =new LogManager();
            _m._connManager =new ConnManager();
            _m._recordIdManager =new RecordIdManager();
        }

        #region Private fields

        private IDataAccessorMgnt _accessorMgnt;
        private ILogManager _logManager;
        private IConnManager _connManager;
        private IRecordIdManager _recordIdManager;
        #endregion

        #region Static properties

        /// <summary>
        /// 提示信息管理器
        /// </summary>
        /// <value>The tip manager.</value>
        public static IDataAccessorMgnt DbAccessorMgnt
        {
            get { return _m == null ? null : _m._accessorMgnt; }
        }

        /// <summary>
        /// 日志记录器
        /// </summary>
        /// <value>The log manager.</value>
        public static ILogManager LogManager
        {
            get { return _m == null ? null : _m._logManager; }
        }

        /// <summary>
        /// 连接字符串管理器
        /// </summary>
        /// <value>The log manager.</value>
        public static IConnManager ConnManager
        {
            get { return _m == null ? null : _m._connManager; }
        }

        /// <summary>
        /// 记录Id管理器
        /// </summary>
        /// <value>The plugin manager.</value>
        public static IRecordIdManager RecordIdManager
        {
            get { return _m == null ? null : _m._recordIdManager; }
        }

        #endregion
    }
}
