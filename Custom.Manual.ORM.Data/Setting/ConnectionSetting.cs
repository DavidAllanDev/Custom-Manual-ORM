namespace Custom.Manual.ORM.Data.Setting
{
    public class ConnectionSetting : IConnectionSetting
    {
        public string Database { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string User { get; set; }
        public bool HasEncryption { get; set; }
    }
}
