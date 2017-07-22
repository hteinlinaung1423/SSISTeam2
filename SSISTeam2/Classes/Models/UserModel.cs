using System.Linq;
using System.Web.Security;
using System.Collections.Generic;
using System;


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
            //this.role = Roles.GetRolesForUser(username).First().ToString();
            if (role == null)
            {
                this.role = "Employee";
            }
        }

        public UserModel FindDeptHead()
        {
            SSISEntities context = new SSISEntities();

            string username = "";
            Department dept = this.department;
            List<Dept_Registry> allDeptEmp = context.Dept_Registry.Where(x => x.dept_code == dept.dept_code).ToList();
            foreach (Dept_Registry i in allDeptEmp)
            {
                if (Roles.GetRolesForUser(i.username).First().ToString() == "DeptHead")
                {
                    username = i.username;
                    break;
                }
            }

            UserModel deptHead = new UserModel(username);
            return deptHead;
        }

        public List<UserModel> FindAllDeptUser()
        {
            SSISEntities context = new SSISEntities();
            string dept = this.department.dept_code;
            List<Dept_Registry> regList = context.Dept_Registry.Where(x => x.dept_code == dept).ToList();
            List<UserModel> deptList = new List<UserModel>();
            foreach (Dept_Registry i in regList)
            {
                UserModel user = new UserModel(i.username);
                deptList.Add(user);
            }
            return deptList;
        }

        public UserModel FIndDelegateHead()
        {
            DateTime today = DateTime.Today;
            SSISEntities context = new SSISEntities();
            string dept = this.department.dept_code;
            List<Approval_Duties> approvedList= context.Approval_Duties.Where(x => x.dept_code == dept).ToList();
            List<Approval_Duties> validList = new List<Approval_Duties>();
            for (int i = 0; i < approvedList.Count; i++)
            {
                if (approvedList[i].start_date < today && approvedList[i].end_date > today)
                {
                    validList.Add(approvedList[i]);
                }
            }

            DateTime currentApproved = validList.Max(x => x.created_date);
            Approval_Duties currentRep = context.Approval_Duties.Where(x => x.created_date == currentApproved).ToList().First();
            UserModel repUser = new UserModel(currentRep.username);
            return repUser;
        }

        public UserModel FindDeptRep()
        {
            SSISEntities context = new SSISEntities();
            Department dept = context.Departments.Where(x => x.dept_code == this.department.dept_code).ToList().First();
            string repUser = dept.rep_user;
            UserModel repUserModel = new UserModel(repUser);
            return repUserModel;
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