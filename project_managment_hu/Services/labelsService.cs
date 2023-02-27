
using Microsoft.EntityFrameworkCore;
using project_managment_hu.DbContest;
using project_managment_hu.Dto;
using project_managment_hu.Model;

namespace project_managment_hu.Services
{
    public class labelsService : IlabelsService
    {

        UserContext _contest;
        public labelsService(UserContext contest)
        {
            _contest = contest;
        }

        public ResponseModel CreateLabel(labelsDto labelsDto)
        {
            ResponseModel model = new ResponseModel();
            try
            {

                labels labelsDto1 = new labels();
                labelsDto1.Name = labelsDto.Name;
                _contest.Add<labels>(labelsDto1);
                model.Messsage = "Label Added Successfully";
                // }
                _contest.SaveChanges();
                model.IsSuccess = true;

            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }
         public ResponseModel Deletelebel(int lebellId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                labels _temp = _contest.Find<labels>(lebellId);
                if (_temp != null)
                {
                    _contest.Remove<labels>(_temp);
                    _contest.SaveChanges();
                    model.IsSuccess = true;
                    model.Messsage = "label Deleted Successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "label Not Found";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }
        public ResponseModel AddLabelToIssue(int issueId, int labelId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var issue = _contest.issuses.FirstOrDefault(u => u.IssueId == issueId);
                var label = _contest.labels.FirstOrDefault(u => u.labelId == labelId);



                if (issue != null && label != null)
                {

                    var issueLabel = new IssueLabel
                    {
                        Issue = issue,
                        Label = label
                    };

                    _contest.IssueLabels.Add(issueLabel);
                    model.Messsage = "Label Added Successfully";

                    _contest.SaveChanges();
                    model.IsSuccess = true;


                }
                else if (issue == null)
                {
                    model.Messsage = ($"Issue with ID  does not exist.");
                }
                else
                {
                    model.Messsage = ($"Issue with ID  does not exist.");

                }
                          }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;

        }


        public List<labels> GetAllLabels()
        {
            List<labels> labelList;
            try
            {
                labelList = _contest.labels.Include(s => s.IssueLabels)
               // .ThenInclude(issueLabel => issueLabel.Label)
                .ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return labelList;
        }

        public ResponseModel RemoveLabelToIssue(int issueId, int labelId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var issueLabel = _contest.IssueLabels
           .SingleOrDefault(il => il.IssueId == issueId && il.LabelId == labelId);

                if (issueLabel != null)
                {

                    _contest.IssueLabels.Remove(issueLabel);
                    _contest.SaveChanges();
                    model.IsSuccess = true;
                    model.Messsage = "label Deleted Successfully";


                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "Label  with this issue id Not Found";
                }
                          }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                //model.Messsage = "Error : " + ex.Message;
            }
            return model;

        }

        public List<Issuses> GetIssuesByLabelId(int labelId)
        {
            var issues = _contest.issuses
            .Where(i => i.IssueLabels.Any(il => il.LabelId == labelId))
            .Include(s => s.IssueLabels)
            .ThenInclude(issueLabel => issueLabel.Label)

            .ToList();

            return issues;
        }


    }
}