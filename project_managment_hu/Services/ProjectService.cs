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

                Projects projects = new Projects();
                projects.project_description = projectDto.project_description;
                projects.project_createrId = projectDto.project_createrId;

                _context.Add<Projects>(projects);
                model.Messsage = "Project Added Successfully";
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

        public ResponseModel ProjectDetailsUpdate(int projectId, ProjectDto projectDto)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Projects _temp = _context.Find<Projects>(projectId);
                if (_temp != null)
                {
                    _temp.project_description = projectDto.project_description;
                    _temp.project_createrId = projectDto.project_createrId;

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