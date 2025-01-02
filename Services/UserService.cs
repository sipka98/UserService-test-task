public class UserService
{
    private readonly ValidationService _validationService;

    public UserService(ValidationService validationService)
    {
        _validationService = validationService;
    }

    public void CreateUser(string name, string email, string password, string role)
    {
        _validationService.ValidateName(name);
        _validationService.ValidateEmail(email);
        _validationService.ValidatePassword(password);
        _validationService.ValidateRole(role);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        using (var db = new SqlConnection("connectionString"))
        {
            db.Open();
            var command = new SqlCommand(
                $"INSERT INTO Users (Name, Email, PasswordHash, Role) VALUES ('{name}', '{email}', '{passwordHash}', '{role}')", 
                db
            );
            command.ExecuteNonQuery();
        }
    }

    public List<string> GetUsers()
    {
        var users = new List<string>();

        using (var db = new SqlConnection("connectionString"))
        {
            db.Open();
            var command = new SqlCommand("SELECT Name FROM Users", db);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(reader.GetString(0));
                }
            }
        }

        return users;
    }

    public void UpdateUserRole(int userId, string newRole)
    {
        _validationService.ValidateRole(newRole);

        using (var db = new SqlConnection("connectionString"))
        {
            db.Open();
            var command = new SqlCommand($"UPDATE Users SET Role = '{newRole}' WHERE Id = {userId}", db);
            command.ExecuteNonQuery();
        }
    }
}