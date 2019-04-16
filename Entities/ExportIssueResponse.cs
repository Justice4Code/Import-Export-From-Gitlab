using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ExportIssueResponse
    {
        public int? project_id { get; set; }
        public Milestone milestone { get; set; }
        public Author author { get; set; }
        public string description { get; set; }
        public string state { get; set; }
        public int? iid { get; set; }
        public List<object> labels { get; set; }
        public int? id { get; set; }
        public string title { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? closed_at { get; set; }
        public ClosedBy closed_by { get; set; }
        public int? user_notes_count { get; set; }
        public string due_date { get; set; }
        public string web_url { get; set; }
        public bool? confidential { get; set; }
        public object weight { get; set; }
        public List<ExportNoteResponse> Notes { get; set; }
    }
    public class Milestone
    {
        public object due_date { get; set; }
        public int? project_id { get; set; }
        public string state { get; set; }
        public string description { get; set; }
        public int? iid { get; set; }
        public int? id { get; set; }
        public string title { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }

    public class Author
    {
        public string state { get; set; }
        public string name { get; set; }
    }

    public class ClosedBy
    {
        public string state { get; set; }
        public string name { get; set; }
    }
}
