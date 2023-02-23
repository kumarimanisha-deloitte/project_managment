using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_managment_hu.Dto;
using project_managment_hu.Model;

namespace project_managment_hu.Services
{
    public interface IIssueService
    {
    ResponseModel IssueCreate(IssueDto issueDto);
    public List<Issuses> GetIssuesList();
    public List<Issuses> GetIssueDetailsById(int issueId);




        
    }
}