namespace ProductVersioning.Services
{
    public interface IVersionService
    {
        string IncrementVersion(string releaseTypeStr);
    }
}
