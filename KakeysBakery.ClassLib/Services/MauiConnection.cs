using KakeysBakeryClassLib.Data;
using SQLite;

namespace KakeysBakeryClassLib.Services
{
    public static class MauiConnection
    {
        private static SQLiteConnection _conn;
        private static string baseDataDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)); //change this to be something that will work on any device type
        private static string databaseName = "KakeysBakery";

        private static void Init()
        {
            if (_conn is not null)
                return;

            _conn = new SQLiteConnection(Path.Combine(baseDataDirectory, databaseName));
            _conn.CreateTable<Addon>();
            _conn.CreateTable<Basegood>();
            _conn.CreateTable<Cart>();
            _conn.CreateTable<Customer>();
            _conn.CreateTable<Product>();
            _conn.CreateTable<ProductAddon>();
            _conn.CreateTable<Purchase>();
            _conn.CreateTable<PurchaseProduct>();
            _conn.CreateTable<Referencephoto>();
        }
        public static SQLiteConnection Get()
        {
            Init();
            return _conn;
        }

    }
}
