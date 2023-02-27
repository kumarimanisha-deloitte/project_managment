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

        public ResponseModel AssignIssueToUser(int issueId, int userId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                // Get the issue from the database
                var issue = _context.issuses.FirstOrDefault(u => u.IssueId == issueId);

                // Get the user from the database
                var user = _context.userModels.FirstOrDefault(u => u.Id == userId);

                // Update the issue with the assigned user ID
                
                if (issue != null && user != null)
                {
                    issue.AssigneeId = userId;
                    issue.UpdateTime = DateTime.UtcNow;


                    // Save the changes to the database
                    model.Messsage = "Assignee Added Successfully";
                    // }
                    _context.SaveChanges();



                    model.IsSuccess = true;
                }
                else if (issue == null)
                {
                    model.IsSuccess = false;
                    model.Messsage = ($"Issue with ID  does not exist.");

                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = ($"User with ID  does not exist.");

                }


            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public List<Issuses> GetIssueDetailsById(int issueId)
        {
            Issuses issuses;
            List<Issuses> issuses1;

            try
            {
                // emp = _context.Find < Employee > (empId);
                issuses1 = _context.issuses.Include(s => s.projects).Include(s => s.Reporter).Include(s => s.Assignee).Include(s => s.IssueLabels).ThenInclude(issueLabel => issueLabel.Label)
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

        public List<Issuses> GetIssuesOnTitleAndDescription(string title, string description)
        {
            List<Issuses> issuses;
            issuses = _context.issuses
                         .Where(i => i.IssueTitle.Contains(title) && i.IssueDescription.Contains(description))
                          .ToList();

            return issuses;
        }


        public ResponseModel IssueCreate(IssueDto issueDto)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var project = _context.projects.FirstOrDefault(p => p.ProjectId == issueDto.projectsProjectId);
                // Check if the reporter exists
                var reporter = _context.userModels.FirstOrDefault(u => u.Id == issueDto.ReporterId);
                // Check if the assignee exists
                //var assignee = _context.userModels.FirstOrDefault(u => u.Id == issueDto.AssigneeId);
                if (project != null && reporter != null)
                {

                    Issuses issues = new Issuses();

                    issues.IssueId = issueDto.IssueId;
                    issues.IssueType = ((IssueType)issueDto.IssueType).ToString();
                    issues.IssueTitle = issueDto.IssueTitle;
                    issues.IssueDescription = issueDto.IssueDescription;
                    issues.Status = IssueStatus.Open.ToString();
                    //issues.ReporterId = issueDto.ReporterId;
                    issues.Reporter = reporter;

                    // issues.AssigneeId = issueDto.AssigneeId;
                    //issues.projectId=issueDto.projectId;
                    // issues.projectsProjectId = issueDto.projectsProjectId;
                    issues.projects = project;
                    issues.CreateTime = DateTime.UtcNow;
                    issues.UpdateTime = DateTime.UtcNow;
                    _context.Add<Issuses>(issues);
                    model.Messsage = "Issue Added Successfully";
                    // }
                    _context.SaveChanges();
                    // Check if the project exists

                    model.IsSuccess = true;
                }
                else if (project == null)
                {
                    model.Messsage = ($"Project with ID {issueDto.projectsProjectId} does not exist.");

                }
                else if (reporter == null)
                {
                    model.Messsage = ($"User with ID {issueDto.ReporterId} does not exist.");
                }
                else
                {
                    model.Messsage = ($"User with ID {issueDto.AssigneeId} does not exist.");

                }
            }
            catch (Exception ex)
            {

                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;

        }

        public ResponseModel IssueDetailsUpdate(int issueId, IssueDto issueDto)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                // var issue = _context.issuses.FirstOrDefault(p => p.IssueId == issueDto.IssueId);
                // var project = _context.projects.FirstOrDefault(p => p.ProjectId == issueDto.projectsProjectId);
                // // Check if the reporter exists
                // var reporter = _context.userModels.FirstOrDefault(u => u.Id == issueDto.ReporterId);
                // // Check if the assignee exists
                // var assignee = _context.userModels.FirstOrDefault(u => u.Id == issueDto.AssigneeId);
                // if (issue != null && project != null && reporter != null && assignee != null)
                // {
                //     Issuses issues = new Issuses();

                //     issues.IssueId = issueDto.IssueId;
                //     issues.IssueType = ((IssueType)issueDto.IssueType).ToString();
                //     issues.IssueTitle = issueDto.IssueTitle;
                //     issues.IssueDescription = issueDto.IssueDescription;
                //     // issues.Status = ((IssueStatus)issueDto.Status).ToString();
                //     // issues.ReporterId = issueDto.ReporterId;
                //     //issues.AssigneeId = issueDto.AssigneeId;
                //     //issues.projectId=issueDto.projectId;
                //     // issues.projectsProjectId = issueDto.projectsProjectId;
                //    // issues.CreateTime = DateTime.UtcNow;
                //     issues.UpdateTime = DateTime.UtcNow;
                //     _context.Update<Issuses>(issues);
                //     model.Messsage = "Issue Update Successfully";
                //     // }
                //     _context.SaveChanges();
                //     // Check if the project exists

                //     model.IsSuccess = true;
                // }
                // else if (issue == null)
                // {
                //     model.IsSuccess = false;
                //     model.Messsage = ($"Issue with ID {issueDto.IssueId} does not exist.");

                // }
                // else if (project == null)
                // {
                //     model.IsSuccess = false;
                //     model.Messsage = ($"Project with ID {issueDto.projectsProjectId} does not exist.");

                // }
                // else if (reporter == null)
                // {
                //     model.IsSuccess = false;
                //     model.Messsage = ($"User with ID {issueDto.ReporterId} does not exist.");

                // }
                // else
                // {
                //     model.IsSuccess = false;
                //     model.Messsage = ($"User with ID {issueDto.AssigneeId} does not exist.");

                // }
                Issuses _temp = _context.Find<Issuses>(issueId);
                if (_temp != null)
                {
                    _temp.IssueTitle = issueDto.IssueTitle;
                    _temp.IssueDescription=issueDto.IssueDescription;
                    _temp.UpdateTime=DateTime.UtcNow;
                    _temp.IssueType=((IssueType)issueDto.IssueType).ToString();
                   // _temp.project_createrId = projectDto.project_createrId;

                    _context.Update<Issuses>(_temp);
                    model.Messsage = "issue Updated Successfully";
                    // }
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "issue Not Found";
                }

            }
            catch (Exception ex)
            {

                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;

        }
        public ResponseModel DeleteIssue(int issueId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Issuses _temp = _context.Find<Issuses>(issueId);
                if (_temp != null)
                {
                    _context.Remove<Issuses>(_temp);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Messsage = "Issue Deleted Successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "Issue Not Found";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public ResponseModel UpdateIssueStatus(int issueId)
        {
            ResponseModel model = new ResponseModel();
            try
            {

                Issuses issuses = _context.Find<Issuses>(issueId);
                if (issuses == null)
                {
                    model.Messsage = "Issue not exists";
                }

                // increment the status by one if it's not the last status in the enum
                // if (issuses.Status != IssueStatus.Done.ToString())
                // {
                //     int(issuses.Status)++;
                // }
                var currentStatus = issuses.Status;


                // Convert the enum value to a string



                // Determine the next valid status in the sequence
                var newStatus = "";

                switch (currentStatus)
                {
                    case "Open":
                        newStatus = (IssueStatus.InProgress).ToString();
                        break;
                    case "InProgress":
                        newStatus = (IssueStatus.InReview).ToString();
                        break;
                    case "InReview":
                        newStatus = (IssueStatus.CodeComplete).ToString();
                        break;
                    case "CodeComplete":
                        newStatus = (IssueStatus.QATesting).ToString();
                        break;
                    case "QATesting":
                        newStatus = (IssueStatus.Done).ToString();
                        break;
                    case "Done":
                        // If the current status is Done, there is no valid next status
                        model.Messsage = "The issue is already in the final status.";
                        break;
                    default:
                        model.Messsage = "Invalid status.";
                        break;
                }

                // Update the issue status
                issuses.Status = newStatus;
                issuses.UpdateTime = DateTime.UtcNow;

                _context.SaveChanges();

                // _context.Add<Issuses>(issuses);
                model.Messsage = "Issue updated Successfully";
                // }
                // _context.SaveChanges();
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public List<Issuses> GetIssuesForProjectOrAssignee(int projectId, string assigneeEmail)
        {
            // Projects _temp = _context.Find<Projects>(projectId);
            //if(_temp!=null)
            var issues = _context.issuses
            .Where(i => i.projectsProjectId == projectId || i.Assignee.EmailAddress == assigneeEmail)
            .Include(i => i.Assignee)
            .Include(i => i.IssueLabels).ThenInclude(il => il.Label)
            .ToList();

            return issues;


        }

        public List<Issuses> GetIssuesForProjectAndAssignee(int projectId, string assigneeEmail)
        {
            var issues = _context.issuses
                .Where(i => i.projectsProjectId == projectId && i.Assignee.EmailAddress == assigneeEmail)
                 .Include(i => i.IssueLabels).ThenInclude(il => il.Label)
                 .ToList();

            // Map the issues to DTOs

            return issues;
        }

        public List<Issuses> GetIssuesByType(string type)
        {
            var issues = _context.issuses.Where(i => i.IssueType.ToLower() == type.ToLower()).ToList();

            return issues;

        }

        public List<Issuses> GetIssuesGreaterThanCreatedDate(DateTime date)
        {
            var issuesList= _context.issuses.Where(d=>DateTime.Compare(date,d.CreateTime)<=0).ToList();
            return issuesList;
        }


        public List<Issuses> GetIssuesLowerThanUpdatedDate(DateTime date)
        {
            var issuesList= _context.issuses.Where(d=>DateTime.Compare(date,d.UpdateTime)>=0).ToList();
            return issuesList;
        }
    }
}