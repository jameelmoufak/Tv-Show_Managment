namespace Tv_Show_Managment.Repositories
{
    public interface IStateRepository
    {
        string GetValue (string key);
        void SetValue (string key, string value);
        void Delete (string key);
    }
}
