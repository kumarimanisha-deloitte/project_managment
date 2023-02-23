using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project_managment_hu.Dto;
using project_managment_hu.Model;
using project_managment_hu.Services;

namespace project_managment_hu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueController : ControllerBase
    {
        IIssueService _issueService;
        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpPost]
        [Route("[action]")]
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
        // [Authorize(Roles="admin")]
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
        public List<Issuses> GetIssueDetailByID(int issueId)
        {

            return _issueService.GetIssueDetailsById(issueId);
            // return  emp;
        }

    }
}