﻿using DAL.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repository
{
	public class CompanyRepository : BaseRepository<Company>
	{
        public CompanyRepository(IContextManager contextManager) : base(contextManager)
        {
            
        }
	}
}
