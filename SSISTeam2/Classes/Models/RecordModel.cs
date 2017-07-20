using SSISTeam2.Classes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.Models
{
    public class RecordModel
    {
        private int requestId;
        private UserModel userModel;
        private DateTime date;
        private Dictionary<ItemModel, int> items;
        private Department department;

        public RecordModel()
        {

        }

        public virtual int RequestId
        {
            get
            {
                return requestId;
            }

            set
            {
                if (true)//requestId == -1) // Hasn't been assigned
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

        public Dictionary<ItemModel, int> Items
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

        internal UserModel UserModel
        {
            get
            {
                return userModel;
            }

            set
            {
                userModel = value;
            }
        }

        public Department Department
        {
            get
            {
                return department;
            }

            set
            {
                department = value;
            }
        }

        public DateTime getDate()
        {
            return Date;
        }

        public Department getDepartment()
        {
            return Department;
        }
        public UserModel getUserModel()
        {
            return UserModel;
        }
    }
}
