﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.Models
{
    public class RequestStatus
    {
        public static string PENDING = "Pending";
        public static string APPROVED = "Approved";
        public static string REJECTED = "Rejected";
        public static string DISBURSED = "Disbursed";
        public static string PART_DISBURSED = "PartDisbursed";
        public static string CANCELLED = "Cancelled";
        public static string UPDATED = "Updated";

        private static List<string> statuses = new List<string>(new string[]{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });

        public static bool requestHasHadStatus(RequestModel request, string status)
        {
            int index = statuses.IndexOf(status);
            foreach (string statusCompare in statuses.Skip(index))
            {
                if (request.Status == statusCompare)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class RequestServiceStatus
    {
        public static string PENDING = "Pending";
        public static string APPROVED = "Approved";
        public static string REJECTED = "Rejected";
        public static string DISBURSED = "Disbursed";
        public static string CANCELLED = "Cancelled";
        public static string UPDATED = "Updated";

        public static string ALLOCATED = "Allocated";
        public static string DISBURSING = "Disbursing";

    }

    //public enum RequestServiceStatus
    //{
    //    PENDING,
    //    APPROVED,
    //    REJECTED,
    //    DISBURSED,
    //    //PART_DISBURSED,
    //    CANCELLED,
    //    UPDATED,

    //    ALLOCATED,
    //    DISBURSING
    //    //PART_ALLOCATED,

    //}

    //public enum RequestStatus
    //{

    //    PENDING,
    //    APPROVED,
    //    REJECTED,
    //    DISBURSED,
    //    PART_DISBURSED,
    //    CANCELLED,
    //    UPDATED
    //}
}