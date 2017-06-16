namespace WallE.Data.MySql
{
    public class RecordIdModel
    {
        public string DbName { get; set; }

        public string TableName { get; set; }

        public int CurRecordId { get; set; }
    }
}
