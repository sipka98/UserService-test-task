namespace Services
{
    public interface IValidationService
    {
        void ValidateName(string name);
        void ValidateEmail(string email);
        void ValidatePassword(string password);
        void ValidateRole(string role);
    }
}