using System;
using training.Model;

namespace training.Services
{
	public class StudentService
	{
        private readonly studentDbContext _studentDbContext;

        public StudentService(studentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }



    }
}

