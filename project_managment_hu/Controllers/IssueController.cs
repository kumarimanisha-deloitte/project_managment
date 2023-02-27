using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_managment_hu.DbContest;
using project_managment_hu.Dto;
using project_managment_hu.Model;
using project_managment_hu.Services;




namespace project_managment_hu.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]

    [ApiController]
    [Route("api/[controller]")]
    public class IssueController : ControllerBase
    {
        UserContext _context;
        IIssueService _issueService;
        IProjectService _projectService;
        public IssueController(IIssueService issueService, IProjectService projectService, UserContext context
)
        {
            _issueService = issueService;
            _projectService = projectService;
            _context = context;

        }
      //  [HttpGet]

        // public IActionResult GetIssues([FromQuery] string dsql)
        // {
        //     IQueryable<Issuses> query = _context.issuses.Include(i => i.projects).Include(i => i.Assignee).Include(i => i.IssueLabels);

        //     if (!string.IsNullOrEmpty(dsql))
        //     {
        //         var parser = new SearchQueryParser();
        //         var expression = parser.Parse(dsql);

        //         query = query.Where(expression);
        //     }

        //     var issues = query.ToList();

        //     return Ok(issues);
        // }


        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]

        public IActionResult CreateIssue(IssueDto issueDto)
        {
            try
            {
                var model = _issueService.IssueCreate(issueDto);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]
        public IActionResult GetAllIsssues()
        {
            try
            {
                var issues = _issueService.GetIssuesList();
                if (issues == null) return NotFound();
                return Ok(issues);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]/id")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]

        public List<Issuses> GetIssueDetailByID(int issueId)
        {

            return _issueService.GetIssueDetailsById(issueId);
            // return  emp;
        }

        // [HttpPut("{id}/update-status")]
        [HttpPut]
        [Route("[action]/id")]
        [Authorize(Roles = "Project_Manager,Admin")]
        public IActionResult UpdateIssueStatus(int issueId)
        {
            try
            {
                var model = _issueService.UpdateIssueStatus(issueId);
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

        public IActionResult AssignIssueToUser(int issueId, int userId)
        {
            try
            {
                var model = _issueService.AssignIssueToUser(issueId, userId);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpGet("issues/search")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]

        public List<Issuses> SearchIssues(string title, string description)
        {
            List<Issuses> issuses;
            issuses = _issueService.GetIssuesOnTitleAndDescription(title, description);

            return issuses;
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]

        public IActionResult updateIssueDetail(int issueId, IssueDto issueDto)
        {
            try
            {
                var model = _issueService.IssueDetailsUpdate(issueId, issueDto);
                return Ok(model);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]

        public IActionResult DeleteIssue(int issueId)
        {
            try
            {
                var model = _issueService.DeleteIssue(issueId);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]

        public IActionResult GetIssuesForProjectOrAssignee([FromQuery] int projectId, [FromQuery] string assigneeEmail)
        {
            try
            {
                var issues = _issueService.GetIssuesForProjectOrAssignee(projectId, assigneeEmail);
                if (issues == null) return NotFound();
                return Ok(issues);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]
        public IActionResult GetIssuesForProjectAndAssignee([FromQuery] int projectId, [FromQuery] string assigneeEmail)
        {
            try
            {
                var issues = _issueService.GetIssuesForProjectAndAssignee(projectId, assigneeEmail);
                if (issues == null) return NotFound();
                return Ok(issues);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]

        public IActionResult GetIssuesByType([FromQuery] string type)
        {
            try
            {
                var issues = _issueService.GetIssuesByType(type);
                if (issues == null) return NotFound();
                return Ok(issues);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]

        public IActionResult GetIssuesGreaterThanCreatedDate(string currentDate)
        {
            try
            {
                var issues = _issueService.GetIssuesGreaterThanCreatedDate(DateTime.Parse(currentDate));
                if (issues == null) return NotFound();
                return Ok(issues);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Project_Manager,Admin,Normal")]
        public IActionResult GetIssuesLowerThanUpdatedDate(string currentDate)
        {
            try
            {
                var issues = _issueService.GetIssuesLowerThanUpdatedDate(DateTime.Parse(currentDate));
                if (issues == null) return NotFound();
                return Ok(issues);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }







    }
}