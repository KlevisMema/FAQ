namespace FAQ.DAL.Seeders
{
    public class AccountSettings
    {
        public const string SectionName = "Users";
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SurnName { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public int Age { get; set; } = 0;
        public string[] Roles { get; set; } = new string[0];
    }
}