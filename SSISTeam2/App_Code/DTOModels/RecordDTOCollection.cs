using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.App_Code.DTOModels
{
    public class RecordDTOCollection<T> : Collection<T>
    {
        public List<T> getPageList(int currentPage, int perPageNumber)
        {
            throw new NotImplementedException();
        }
        public RecordDTOCollection<T> beforeDate(DateTime date)
        {
            throw new NotImplementedException();
        }
        public RecordDTOCollection<T> afterDate(DateTime date)
        {
            throw new NotImplementedException();
        }
        public RecordDTOCollection<T> betweenDates(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
        public RecordDTOCollection<T> fromDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }
        public RecordDTOCollection<T> fromDepartments(params int[] departmentIds)
        {
            throw new NotImplementedException();
        }
        public RecordDTOCollection<T> byUser(string username)
        {
            throw new NotImplementedException();
        }
        //public List<T> getItems()
        //{
        //    return this.ToList();
        //}
    }
}
