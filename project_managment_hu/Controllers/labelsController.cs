using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project_managment_hu.Dto;
using project_managment_hu.Services;

namespace project_managment_hu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class labelsController : ControllerBase
    {
        IlabelsService _labelsService;
        public labelsController(IlabelsService labelsService)
        {
            _labelsService= labelsService;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateLabel(labelsDto labelsDto)
        {
            try
            {
                var model = _labelsService.CreateLabel(labelsDto);
                return Ok(model);
           }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("[action]")]
        // [Authorize(Roles="admin")]
        public IActionResult GetAlllabels()
        {
            try
            {
                var labels = _labelsService.GetAllLabels();
                if (labels == null) return NotFound();
                return Ok(labels);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult AddLabelToIssue(int issueId, int labelId)
        {
            try
            {
                var model = _labelsService.AddLabelToIssue(issueId, labelId);
                return Ok(model);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
         [HttpDelete]
        [Route("[action]")]
        public IActionResult RemoveLabelToIssue(int issueId, int labelId)
        {
           try
            {
                var model = _labelsService.RemoveLabelToIssue(issueId, labelId);
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
        public IActionResult GetIssuesByLabelId(int labelId)
        {
            try
            {
                var labels = _labelsService.GetIssuesByLabelId(labelId);
                if (labels == null) return NotFound();
                return Ok(labels);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult Deletelebel(int labelId)
        {
            try
            {
                var model = _labelsService.Deletelebel(labelId);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}