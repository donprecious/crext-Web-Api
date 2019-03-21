using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrExtApiCore.Helpers
{
    public enum ReviewKinds
    {
        Feedback, Query
    }
    public enum ReviewActions
    {
        RESCHEDULE, SET_REMINDER, ADD_PAYMENT, ADD_TO_VISIT_LIST
    }

    public enum ReviewStatus
    {
        READ, UNREAD
    }

}
