using SSISTeam2.App_Code.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.App_Code.DTOModels
{
    public class RecordDTO
    {
        private int requestId = -1;
        private UserDTO user;
        private DateTime date;
        private List<ItemDTO> items;

        public virtual int RequestId
        {
            get
            {
                return requestId;
            }

            set
            {
                if (requestId == -1) // Hasn't been assigned
                {
                    requestId = value;
                }
                else
                {
                    throw new IdentifierAlreadySetException();
                }
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public List<ItemDTO> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
            }
        }

        internal UserDTO User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }
    }
}
