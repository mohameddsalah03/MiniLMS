using System.Text.Json.Serialization;

namespace MiniLMS.API.Models;

public class BaseResponse<T>
{
    public bool IsSuccess { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] // if value equal null don't write it in output json
    public string? Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, string[]>? Errors { get; set; }



    public static BaseResponse<T> Success(T data, string? message = null)
        => new() { IsSuccess = true, Data = data, Message = message };

    public static BaseResponse<T> Fail(string message, IDictionary<string, string[]>? errors = null)
        => new() { IsSuccess = false, Message = message, Errors = errors };
}