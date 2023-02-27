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

        ResponseModel UpdateIssueStatus(int issueId);

        public List<Issuses> GetIssuesOnTitleAndDescription(string tittle, string description);

        ResponseModel AssignIssueToUser(int issueId, int userId);
        ResponseModel IssueDetailsUpdate(int id, IssueDto issueDto);

        ResponseModel DeleteIssue(int issueId);

        public List<Issuses> GetIssuesForProjectOrAssignee(int projectId,string assigneeEmail);
        public List<Issuses> GetIssuesForProjectAndAssignee(int projectId,string assigneeEmail);

        public List<Issuses> GetIssuesByType(string type);











    }
}