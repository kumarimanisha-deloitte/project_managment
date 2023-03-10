using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_managment_hu.Dto;
using project_managment_hu.Model;

namespace project_managment_hu.Services
{
    public interface IProjectService
    {
        ResponseModel ProjectCreate(ProjectDto projectDto);

        public List<Projects> GetProjectDetailsById(int projectId);

        public List<Projects> GetProjectList();

        ResponseModel ProjectDetailsUpdate(int id, ProjectDto projectDto);
        ResponseModel DeleteProject(int projectId);

        public List<Issuses> GetIssuesByProjectId(int projectId);

        ResponseModel CreateIssueProjectId(int projectId, IssueDto issueDto);

        ResponseModel DeleteProjectIssueById(int projectId, int issueId);

        public List<Issuses> GetDetailsAnIssueByProjectId(int projectId,int IssueId);




    }
}