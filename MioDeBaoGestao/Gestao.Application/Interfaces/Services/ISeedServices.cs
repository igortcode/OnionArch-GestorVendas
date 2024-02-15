namespace Gestao.Application.Interfaces.Services
{
    public interface ISeedServices
    {
        void AplayMigrations();
        void SeedAdminUser();
        void SeedRoles();
    }
}
