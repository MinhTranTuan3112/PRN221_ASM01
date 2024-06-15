using BusinessObject;
using DataAccess.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IMemberRepository
    {
        Member? Login(string email, string password);
        List<GetMemberDto> GetMembers(string keyword = "");
    }

}
