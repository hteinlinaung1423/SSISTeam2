using SSISTeam2.App_Code.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.App_Code.DTOModels
{
    public class RequestDTO : AllocatedDTO
    {
        private Department department;
        private string reason;
        private string rejectedReason;
        private Boolean rejected;
        private RequestStatus status;
        private bool wasRetrieved;
        private bool wasDisbursed;
        private bool wasAllocated;

        // Methods
        public List<RetrievalDTO> getRetrievals(Ctx context)
        {
            throw new NotImplementedException();
        }

        public List<DisbursementDTO> getDisbursements(Ctx context)
        {
            throw new NotImplementedException();
        }

        public List<AllocatedDTO> getAllocated(Ctx context)
        {
            throw new NotImplementedException();
        }

        public Dictionary<ItemDTO, int> getShortfall(DisbursementDTO disbursement)
        {
            throw new NotImplementedException();
        }
        public Dictionary<ItemDTO, int> getShortfall(AllocatedDTO allocated)
        {
            throw new NotImplementedException();
        }
        public Dictionary<ItemDTO, int> getShortfall(RetrievalDTO retrieved)
        {
            throw new NotImplementedException();
        }


        // Properties
        public string Reason
        {
            get
            {
                return reason;
            }

            set
            {
                reason = value;
            }
        }

        public string RejectedReason
        {
            get
            {
                return rejectedReason;
            }

            set
            {
                rejectedReason = value;
            }
        }

        public bool WasRetrieved
        {
            get
            {
                return wasRetrieved;
            }

        }

        public bool WasDisbursed
        {
            get
            {
                return wasDisbursed;
            }

        }

        public bool WasAllocated
        {
            get
            {
                return wasAllocated;
            }

        }
    }
}
