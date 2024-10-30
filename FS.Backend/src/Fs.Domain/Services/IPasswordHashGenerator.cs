namespace Fs.Domain.Services
{
    public interface IPasswordHashGenerator
    {
        string Generate(string password, string salt);
        (string Hash, string Salt) Generate(string password);
    }
}
