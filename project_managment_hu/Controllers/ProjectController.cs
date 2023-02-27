using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_managment_hu.Dto;
using project_managment_hu.Model;
using project_managment_hu.Services;

namespace project_managment_hu.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]

    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        IProjectService projectService;
        public ProjectController(IProjectService _projectService)
        {
            projectService = _projectService;
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin")]
        public IActionResult CreateProject(ProjectDto projecttDto)
        {
            try
            {
                var model = projectService.ProjectCreate(projecttDto);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        // [Authorize(Roles="admin")]
        [Authorize(Roles = "Project_Manager,Admin")]
        public IActionResult GetAllProjects()
        {
            try
            {
                var projects = projectService.GetProjectList();
                if (projects == null) return NotFound();
                return Ok(projects);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]/id")]
        [Authorize(Roles = "Project_Manager,Admin")]
        public List<Projects> GetProjectByID(int projectId)
        {

            return projectService.GetProjectDetailsById(projectId);
            // return  emp;
        }
        [HttpGet]
        [Route("[action]/id")]
        [Authorize(Roles = "Project_Manager,Admin")]

        public List<Issuses> GetIssuesByProjectId(int projectId)
        {

            return projectService.GetIssuesByProjectId(projectId);
            // return  emp;
        }


        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin")]

        public IActionResult DeleteProject(int peojectId)
        {
            try
            {
                var model = projectService.DeleteProject(peojectId);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin")]

        public IActionResult updateProjectDetail(int projectId, ProjectDto projectDto)
        {
            try
            {
                var model = projectService.ProjectDetailsUpdate(projectId, projectDto);
                return Ok(model);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]

        public IActionResult CreateIssueProjectId(int projectId, IssueDto issueDto)
        {
            try
            {
                var model = projectService.CreateIssueProjectId(projectId, issueDto);
                return Ok(model);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin")]
        public IActionResult DeleteProjectIssueById(int peojectId,int issueId)
        {
            try
            {
                var model = projectService.DeleteProjectIssueById(peojectId,issueId);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}