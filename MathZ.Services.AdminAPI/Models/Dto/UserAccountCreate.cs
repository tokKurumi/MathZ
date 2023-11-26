namespace MathZ.Services.AdminAPI.Models.Dto
{
    public class UserAccountCreate(string login, string password, string name, string surname, string[] roles)
    {
        public string Login { get; set; } = login;

        public string Password { get; set; } = password;

        public string Name { get; set; } = name;

        public string Surname { get; set; } = surname;

        public string[] Roles { get; set; } = roles;
    }
}