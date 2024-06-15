﻿using BusinessObject;
using DataAccess.DAO;
using DataAccess.DTOs.Member;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public List<GetMemberDto> GetMembers(string keyword = "")
        {
            return MemberDAO.Instance.GetMembers(keyword).Adapt<List<GetMemberDto>>();
        }

        public Member? Login(string email, string password)
        {
            return MemberDAO.Instance.Login(email, password);
        }
    }
}