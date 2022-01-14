namespace DougScoreViewerAPI.Models;

public class ApiBaseResponse
{
    public string? Message { get; set; }
    
    public bool Successful { get; set; }
}

public class ApiResponse<T> : ApiBaseResponse
{
    public T? Data { get; set; }
}