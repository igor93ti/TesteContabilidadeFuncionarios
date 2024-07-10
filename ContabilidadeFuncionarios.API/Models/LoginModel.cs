namespace ContabilidadeFuncionarios.API.Models
{
    public class LoginModel(string username, string Password)
    {
        public string Username { get; set; } = username;

        public string Password { get; set; } = Password;
    }
}
