namespace Gestao.Core.Interfaces.Services
{
    public interface ISeedServices
    {
        void AplayMigrations();
        void SeedAdminUser();
        void SeedRoles();
    }
}
