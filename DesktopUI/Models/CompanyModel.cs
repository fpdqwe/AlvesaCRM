using DAL;
using DAL.Repository;
using Domain.Entities;
using Domain.Enums;

namespace DesktopUI.Models
{
    public class CompanyModel
    {
        private CompanyRepository _companyRepository;
        public CompanyModel()
        {
            
            var contextManager = new ContextManager();
            _companyRepository = new CompanyRepository(contextManager);
        }

        public async Task<bool> UpdateCompany(Company company)
        {
            try
            {
                await _companyRepository.Update(company);
            }
            catch (Exception ex) { return false; }
            return true;
        }
    }
}
