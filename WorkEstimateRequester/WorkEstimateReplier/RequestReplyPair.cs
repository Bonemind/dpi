using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkEstimateReplier
{
    class RequestReplyPair
    {
        /// <summary>
        /// The workrequest for this pair
        /// </summary>
        public WorkRequest Workrequest { get; set; }

        /// <summary>
        /// The workresponse for this pair
        /// </summary>
        public WorkResponse Workresponse { get; set; }

        /// <summary>
        /// Determines if we have a reply, convenience method
        /// </summary>
        /// <returns>Whether this request has a response</returns>
        public bool hasResponse()
        {
            return this.Workresponse != null;
        }

        /// <summary>
        /// Tostring override for listbox display
        /// </summary>
        /// <returns>A string representation of this pair</returns>
        public override string ToString()
        {
            if (Workrequest == null)
            {
                return "INVALID";
            }
            return Workrequest.Worktype.ToString();
        }
    }
}
