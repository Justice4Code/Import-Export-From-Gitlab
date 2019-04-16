using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ImportCreateIssueResponse
    {
        public int? project_id { get; set; }
        public int? id { get; set; }
        public DateTime? created_at { get; set; }
        public int? iid { get; set; }
        public string title { get; set; }
        public string state { get; set; }
        public List<object> assignees { get; set; }
        public object assignee { get; set; }
        public List<string> labels { get; set; }
        public int? upvotes { get; set; }
        public int? downvotes { get; set; }
        public Author author { get; set; }
        public object description { get; set; }
        public DateTime? updated_at { get; set; }
        public object closed_at { get; set; }
        public object closed_by { get; set; }
        public object milestone { get; set; }
        public bool? subscribed { get; set; }
        public int? user_notes_count { get; set; }
        public object due_date { get; set; }
        public string web_url { get; set; }
        public TimeStats time_stats { get; set; }
        public bool? confidential { get; set; }
        public object weight { get; set; }
        public bool? discussion_locked { get; set; }
        public Links _links { get; set; }
    }
    public class TimeStats
    {
        public int? time_estimate { get; set; }
        public int? total_time_spent { get; set; }
        public object human_time_estimate { get; set; }
        public object human_total_time_spent { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
        public string notes { get; set; }
        public string award_emoji { get; set; }
        public string project { get; set; }
    }
}
