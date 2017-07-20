using System.Linq;
using System.Web.Security;


namespace SSISTeam2.Classes.Models
{
    public class UserModel
    {
        private string username;
        private string email;
        private string contactNumber;
        private Department department;
        private string role;

        public UserModel(string username)
        {
            SSISEntities context = new SSISEntities();
            Dept_Registry user = context.Dept_Registry.Where(x => x.username == username).ToList().First();
            Department dept = context.Departments.Where(x => x.dept_code == user.dept_code).ToList().First();
            
                //Membership.FindUsersByName(username);
            

            this.username = user.username;
            this.email = Membership.GetUser(username).Email;
            this.department = dept;
            this.role = Roles.GetRolesForUser(username).First().ToString();
            if (role == null)
            {
                this.role = "Employee";
            }
        }

        //public UserModel
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