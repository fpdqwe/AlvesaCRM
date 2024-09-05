using DAL;
using DAL.Repository;
using DesktopUI.Utilities;
using Domain.Entities;
using Domain.Enums;

namespace DesktopUI.Models
{
    public class CompanyModel
    {
        private CompanyRepository _repository;
        public CompanyModel()
        {
            
            _repository = AuthService.Repository.CompanyRepository;
        }

        public async Task<bool> UpdateCompany(Company company)
        {
            try
            {
                await _repository.Update(company);
            }
            catch (Exception ex) { return false; }
            return true;
        }
    }
}
