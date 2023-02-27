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
    public class ProjectService : IProjectService
    {
        UserContext _context;

        public ProjectService(UserContext context)
        {
            _context = context;
        }

        public ResponseModel CreateIssueProjectId(int projectId, IssueDto issueDto)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Projects _temp = _context.Find<Projects>(projectId);
                if (_temp != null)
                {
                    Issuses issuses = new Issuses();
                    issuses.projectsProjectId = projectId;
                    issuses.Status = IssueStatus.Open.ToString();
                    issuses.IssueType = ((IssueType)issueDto.IssueType).ToString();
                   // issuses.AssigneeId = issueDto.AssigneeId;
                    issuses.ReporterId = issueDto.ReporterId;
                    issuses.CreateTime = DateTime.UtcNow;
                    issuses.UpdateTime = DateTime.UtcNow;
                    issuses.IssueDescription = issueDto.IssueDescription;
                    issuses.IssueTitle = issueDto.IssueTitle;
                    _context.Add<Issuses>(issuses);
                    model.IsSuccess = true;
                    model.Messsage = "issue Created Successfully";
                    _context.SaveChanges();
                    model.IsSuccess = true;

                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "Project Not Found";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public ResponseModel DeleteProject(int projectId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Projects _temp = _context.Find<Projects>(projectId);
                if (_temp != null)
                {
                    _context.Remove<Projects>(_temp);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Messsage = "Project Deleted Successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "Project Not Found";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public ResponseModel DeleteProjectIssueById(int projectId, int issueId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var issue = _context.issuses.FirstOrDefault(i => i.projectsProjectId == projectId && i.IssueId == issueId);

                if (issue != null)
                {
                    _context.Remove<Issuses>(issue);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Messsage = "Issue Deleted Successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "Project  and issue Not Found";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public List<Issuses> GetDetailsAnIssueByProjectId(int projectId, int IssueId)
        {
            Issuses issuses;
            List<Issuses> issuses1;

            try
            {
                
                issuses1 = _context.issuses
                            .Where(s => s.projectsProjectId == projectId && s.IssueId==IssueId).Include(s=>s.Assignee).ToList();
                


            }
            catch (Exception)
            {
                throw;
            }
            return issuses1;
            
        }

        public List<Issuses> GetIssuesByProjectId(int projectId)
        {
            Issuses issuses;
            List<Issuses> issuses1;

            try
            {
                // emp = _context.Find < Employee > (empId);
                issuses1 = _context.issuses
                            .Where(s => s.projectsProjectId == projectId).ToList();


            }
            catch (Exception)
            {
                throw;
            }
            return issuses1;
        }

        public List<Projects> GetProjectDetailsById(int projectId)
        {

            Projects projects;
            List<Projects> projects1;

            try
            {
                // emp = _context.Find < Employee > (empId);
                projects1 = _context.projects.Include(s => s.project_creater).Include(s => s.issuses)
                .Where(s => s.ProjectId == projectId).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return projects1;

        }

        public List<Projects> GetProjectList()
        {

            List<Projects> projectList;
            try
            {
                projectList = _context.projects.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return projectList;

        }

        public ResponseModel ProjectCreate(ProjectDto projectDto)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var project_creater = _context.userModels.FirstOrDefault(u => u.Id == projectDto.project_createrId);

                if (project_creater == null)
                {
                    model.Messsage = ($"User with ID {projectDto.project_createrId} does not exist.");
                }
                if (project_creater != null)
                {
                    Projects projects = new Projects();
                    projects.project_description = projectDto.project_description;
                    projects.project_createrId = projectDto.project_createrId;

                    _context.Add<Projects>(projects);
                    model.Messsage = "Project Added Successfully";
                    // }
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
                else
                {
                    model.IsSuccess = false;

                    model.Messsage = ($"User with ID {projectDto.project_createrId} does not exist.");

                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public ResponseModel ProjectDetailsUpdate(int projectId, ProjectDto projectDto)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Projects _temp = _context.Find<Projects>(projectId);
                if (_temp != null)
                {
                    _temp.project_description = projectDto.project_description;
                   // _temp.project_createrId = projectDto.project_createrId;

                    _context.Update<Projects>(_temp);
                    model.Messsage = "Project Updated Successfully";
                    // }
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "Project Not Found";
                }
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