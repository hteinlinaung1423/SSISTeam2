using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.Models
{
    public class RecordModelCollection<T> : Collection<T>
    {
        public RecordModelCollection(IList<T> list) : base(list)
        {
        }

        public List<T> getPageList(int currentPage, int perPageNumber)
        {
            throw new NotImplementedException();
        }
        public RecordModelCollection<T> beforeDate(DateTime date)
        {
            throw new NotImplementedException();
        }
        public RecordModelCollection<T> afterDate(DateTime date)
        {
            throw new NotImplementedException();
        }
        public RecordModelCollection<T> betweenDates(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
        public RecordModelCollection<T> fromDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }
        public RecordModelCollection<T> fromDepartments(params int[] departmentIds)
        {
            throw new NotImplementedException();
        }
        public RecordModelCollection<T> byUser(string username)
        {
            throw new NotImplementedException();
        }
        //public List<T> getItems()
        //{
        //    return this.ToList();
        //}
    }
}
