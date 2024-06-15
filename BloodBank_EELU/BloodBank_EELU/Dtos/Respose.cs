using System.Collections.Generic;

namespace BloodBank_EELU.Dtos
{
    public class ApiResponse<T>
    {
            //public int NumberOfblood { get; set; }
            public List<T> Data { get; set; }
    }
}
