using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ImportCreateIssueRequest
    {
        public ImportCreateIssueRequest(string projectId, string token, string milestoneId)
        {
            this.ProjectId = projectId;
            this.PrivateToken = token; 
            this.MilestoneId = milestoneId; 
            this.BaseUrl = "https://git.megadevs.org/";
            this.FragmentUrl = "api/v4/projects/" + this.ProjectId + "/issues";

        }
        //numeric project id, can be found in project -> general settings
        public string ProjectId { get; set; }
        //obtained from https://gitlab.com/profile/personal_access_tokens
        public string PrivateToken { get; set; }
        //change if using a private install
        public string BaseUrl { get; set; }
        public string FragmentUrl { get; set; }
        public string IssueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MilestoneId { get; set; } 

    }
}
