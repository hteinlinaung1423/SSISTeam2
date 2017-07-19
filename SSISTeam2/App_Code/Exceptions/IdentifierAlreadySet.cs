using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.App_Code.Exceptions
{
    class IdentifierAlreadySetException : ApplicationException
    {
        public IdentifierAlreadySetException()
        {
        }
        public IdentifierAlreadySetException(String message) : base(message)
        {
        }
        public IdentifierAlreadySetException(String message, Exception inner) : base(message, inner)
        {
        }
    }
}
