using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project_managment_hu.DbContest;
using project_managment_hu.Dto;
using project_managment_hu.Model;

namespace project_managment_hu.Services
{
    public class IssueService : IIssueService
    {
        UserContext _context;

        public IssueService(UserContext context)
        {
            _context = context;
        }

        public List<Issuses> GetIssueDetailsById(int issueId)
        {
            Issuses issuses;
            List<Issuses> issuses1;

            try
            {
                // emp = _context.Find < Employee > (empId);
                issuses1 = _context.issuses.Include(s => s.projects).Include(s => s.Reporter).Include(s=>s.Assignee)
                .Where(s => s.IssueId == issueId).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return issuses1;
        }

        public List<Issuses> GetIssuesList()
        {
            List<Issuses> issuesList;
            try
            {
                issuesList = _context.issuses.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return issuesList;
        }

        public ResponseModel IssueCreate(IssueDto issueDto)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                // Check if the project exists
                var project = _context.projects.FirstOrDefault(p => p.ProjectId == issueDto.projectsProjectId);

                if (project == null)
                {
                    model.Messsage=($"Project with ID {issueDto.projectsProjectId} does not exist.");
                }

                // Check if the reporter exists
                var reporter = _context.userModels.FirstOrDefault(u => u.Id == issueDto.ReporterId);

                if (reporter == null)
                {
                    model.Messsage=($"User with ID {issueDto.ReporterId} does not exist.");
                }

                // Check if the assignee exists
                var assignee = _context.userModels.FirstOrDefault(u => u.Id == issueDto.AssigneeId);

                if (assignee == null)
                {
                    model.Messsage=($"User with ID {issueDto.AssigneeId} does not exist.");
                }

                Issuses issues = new Issuses();

                issues.IssueId = issueDto.IssueId;
                issues.IssueType = issueDto.IssueType;
                issues.IssueTitle = issueDto.IssueTitle;
                issues.IssueDescription = issueDto.IssueDescription;
                issues.Status = issueDto.Status;
                issues.ReporterId = issueDto.ReporterId;
                issues.AssigneeId = issueDto.AssigneeId;
                //issues.projectId=issueDto.projectId;
                issues.projectsProjectId = issueDto.projectsProjectId;
                issues.CreateTime = DateTime.UtcNow;
                issues.UpdateTime = DateTime.UtcNow;
                _context.Add<Issuses>(issues);
                model.Messsage = "Issue Added Successfully";
                // }
                _context.SaveChanges();
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;

        }
    }
}