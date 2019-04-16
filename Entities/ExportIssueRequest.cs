using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ExportIssueRequest
    {
        public ExportIssueRequest(string projectId, string token)
        {
            this.ProjectId = projectId;
            this.PrivateToken = token; 
            this.BaseUrl = "https://gitlab.com/";
            //you may only take 100 issues per call via api from gitlab sadly. 
            this.FragmentUrl = "api/v4/projects/" + this.ProjectId + "/issues?per_page=100";
        }
        //numeric project id, can be found in project -> general settings
        public string ProjectId { get; set; }
        //obtained from https://gitlab.com/profile/personal_access_tokens
        public string PrivateToken { get; set; }
        //change if using a private install
        public string BaseUrl { get; set; }
        public string FragmentUrl { get; set; }
        public string State { get; set; }
        public string Page { get; set; }
    }
}
