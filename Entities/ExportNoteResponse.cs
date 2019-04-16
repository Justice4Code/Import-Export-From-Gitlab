using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ExportNoteResponse
    {
        public int? id { get; set; }
        public string body { get; set; }
        public object attachment { get; set; }
        public Author author { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public bool? system { get; set; }
        public int? noteable_id { get; set; }
        public string noteable_type { get; set; }
        public int? noteable_iid { get; set; }
        public bool? resolvable { get; set; }

    }
}
