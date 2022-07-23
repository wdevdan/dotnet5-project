namespace DW.Company.Entities.Dto
{
    public class LoginDto
    {
        private string _login;
        public string Login
        {
            get
            {
                return _login?.Trim()?.ToLower();
            }
            set
            {
                _login = value;
            }
        }
        public string Password { get; set; }
    }
}
