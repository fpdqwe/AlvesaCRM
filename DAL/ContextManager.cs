﻿using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
	public class ContextManager : IContextManager
	{
		private readonly string _connectionString = "Host=localhost;Username=postgres;Password=admin;Database=Alvesa";
        public ContextManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ApplicationDbContext CreateDatabaseContext()
		{
			return new ApplicationDbContext(_connectionString);
		}
	}
}
