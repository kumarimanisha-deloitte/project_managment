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
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IProjectService _projectService,ILogger<ProjectController> logger)
        {
            projectService = _projectService;
            _logger=logger;
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin")]
        public IActionResult CreateProject(ProjectDto projecttDto)
        {
            try
            {
                if (ModelState.IsValid){

                var model = projectService.ProjectCreate(projecttDto);
                return Ok(model);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "This is my error log message with an exception.");
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
                 _logger.LogError(e, "This is my error log message with an exception.");
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
            catch (Exception e)
            {
                _logger.LogError(e, "This is my error log message with an exception.");
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
                if (ModelState.IsValid){
                var model = projectService.ProjectDetailsUpdate(projectId, projectDto);
                return Ok(model);
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "This is my error log message with an exception.");
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
                if (ModelState.IsValid){

                var model = projectService.CreateIssueProjectId(projectId, issueDto);
                return Ok(model);

                }
                else
                {
                    return BadRequest(ModelState);

                }

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

        [HttpGet]
        [Route("[action]")]
        // [Authorize(Roles="admin")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]
        public IActionResult GetDetailsAnIssueByProjectId(int projectId, int IssueId)
        {
            try
            {
                var issueList = projectService.GetDetailsAnIssueByProjectId(projectId,IssueId);
                if (issueList == null) return NotFound();
                return Ok(issueList);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


    }
}