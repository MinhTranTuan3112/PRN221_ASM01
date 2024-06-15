﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        private readonly SalesManagementDbContext _context;

        private static MemberDAO? _instance = null;

        public MemberDAO()
        {
            if (_context is null)
            {
                _context = new SalesManagementDbContext();
            }
        }

        public static MemberDAO Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new MemberDAO();
                }

                return _instance;
            }
        }

        public Member? Login(string email, string password)
        {
            var member = _context.Members.FirstOrDefault(m => m.Email == email && m.Password == password);

            return member;
        }

        public List<Member> GetMembers(string keyword = "")
        {
            return _context.Members.ToList();
        }
    }
}
