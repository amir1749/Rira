using System;
using System.Collections.Generic;
using System.Text;

namespace Rira.Utility.Framework.Common
{
    public sealed class ErrorResponse
    {
        public int StatusCode { get; set; }

        public string Title { get; set; } = default!;

        public List<string> Errors { get; set; } = [];

        public string TraceId { get; set; } = default!;
    }
}
