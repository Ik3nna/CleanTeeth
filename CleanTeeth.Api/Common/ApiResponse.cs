public class ApiResponse<T>
{
    public int ResponseCode { get; set; }
    public string ResponseMessage { get; set; }
    public T Data { get; set; }

    public ApiResponse(int responseCode, string responseMessage, T data = default)
    {
        ResponseCode = responseCode;
        ResponseMessage = responseMessage;
        Data = data;
    }

    public static ApiResponse<T> Success(T data, string message = "Success", int statusCode = 200)
        => new ApiResponse<T>(statusCode, message, data);

    public static ApiResponse<T> Fail(string message, int statusCode = 500)
        => new ApiResponse<T>(statusCode, message, default);
}
