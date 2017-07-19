namespace SSISTeam2.Classes.Models
{
    public class UserModel
    {
        private string username;
        private string email;
        private string contactNumber;
        private Department department;
        private string role;

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string Role
        {
            get
            {
                return role;
            }

            set
            {
                role = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string ContactNumber
        {
            get
            {
                return contactNumber;
            }

            set
            {
                contactNumber = value;
            }
        }

        internal Department Department
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
    }
}