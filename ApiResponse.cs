using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.ViewModels
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string? Message { get; set; }
        public string? Error { get; set; }
        public int? TotalRecords { get; set; }

        public ApiResponse(bool Success)
        {
            this.Success = Success;
        }
        public ApiResponse(T data, string? message = null,int? totalRecords = 0)
        {
            Success = true;
            Data = data;
            Message = message ?? "Successful.";
            Error = null;
            TotalRecords = totalRecords;
        }

        public ApiResponse(string error)
        {
            Success = false;
            Message = null;
            Error = error;
        }
    }

}
