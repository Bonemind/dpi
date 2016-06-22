using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkEstimateReplier
{ 
    class WorkRequest
    {
        /// <summary>
        /// The id of this message, used to match responses
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The worktype string
        /// </summary>
        public string Worktype { get; set; }

        /// <summary>
        /// A description for the worktype
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The address this request should reply to
        /// </summary>
        public string ReturnAddress { get; set; }

        /// <summary>
        /// Constructor to create a new request
        /// </summary>
        /// <param name="Id">The id of this request</param>
        /// <param name="Worktype">The worktype of this request</param>
        /// <param name="Description">The description of this request</param>
        public WorkRequest(int Id, string Worktype, string Description)
        {
            this.Id = Id;
            this.Worktype = Worktype;
            this.Description = Description;
        }
    }
}
