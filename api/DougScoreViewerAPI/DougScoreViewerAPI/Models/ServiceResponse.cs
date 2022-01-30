using Microsoft.AspNetCore.Connections.Features;

namespace DougScoreViewerAPI.Models;

public class ServiceResponse<T>
{
    public T? Data { get; set; }   
}