using DougScoreViewerAPI.Enums;
using Microsoft.AspNetCore.Connections.Features;

namespace DougScoreViewerAPI.Models;

public class ServiceResponse<T> : ServiceBaseResponse
{
    public T? Data { get; set; }   
}

public class ServiceBaseResponse
{
    public bool Successful => ErrorCode.HasValue == false;
    
    public ServiceErrorCode? ErrorCode { get; set; }
}