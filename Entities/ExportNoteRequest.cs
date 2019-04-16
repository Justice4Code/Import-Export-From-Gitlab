using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ExportNoteRequest
    {
        public ExportNoteRequest(string projectId,string token, string issueId)
        {
            this.ProjectId = projectId;
            this.PrivateToken = token; 
            this.IssueId = issueId;
            this.BaseUrl = "https://gitlab.com/";
            this.FragmentUrl = "api/v4/projects/" + this.ProjectId + "/issues/" + this.IssueId + "/notes?per_page=200";

        }
        //numeric project id, can be found in project -> general settings
        public string ProjectId { get; set; }
        //obtained from https://gitlab.com/profile/personal_access_tokens
        public string PrivateToken { get; set; }
        //change if using a private install
        public string BaseUrl { get; set; }
        public string FragmentUrl { get; set; }
        public string IssueId { get; set; }
    }
}

