using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_managment_hu.Dto;
using project_managment_hu.Model;

namespace project_managment_hu.Services
{
    public interface IlabelsService
    {
        ResponseModel CreateLabel(labelsDto labelsDto);

        public List<labels> GetAllLabels();
        ResponseModel AddLabelToIssue(int issueId, int labelId);
        ResponseModel RemoveLabelToIssue(int issueId, int labelId);

        public List<Issuses> GetIssuesByLabelId(int labelId);

        ResponseModel Deletelebel(int labelId);

       // public 



        

    }

    
}