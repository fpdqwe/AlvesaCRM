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

        public bool ChangeCompanyType(Company company)
        {

            _companyRepository.Update(company);
            return true;
        }
    }
}
