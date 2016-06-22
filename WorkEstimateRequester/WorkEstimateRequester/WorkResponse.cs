using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkEstimateRequester
{
    class WorkResponse
    {
        /// <summary>
        /// The id of the request this response is for
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// When this work item will be done
        /// </summary>
        public DateTime estimatedDone { get; set; }

        /// <summary>
        /// Any additional notes for this work
        /// </summary>
        public string Notes { get; set; }
    }
}
