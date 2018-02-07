namespace Custom.Manual.ORM.Data.Setting
{
    public interface IConnectionSetting
    {
        string Database { get; set; }
        string Password { get; set; }
        string Server { get; set; }
        string User { get; set; }

        bool HasEncryption { get; set; }
    }
}
