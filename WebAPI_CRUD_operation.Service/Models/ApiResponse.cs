﻿namespace WebAPI_CRUD_operation.Service.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
