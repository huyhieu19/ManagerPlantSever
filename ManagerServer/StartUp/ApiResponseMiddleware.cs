using Newtonsoft.Json;
using System.Text;

namespace ManagerServer.StartUp
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            try
            {
                // Tạo một Stream để ghi dữ liệu phản hồi
                using ( var responseBody = new MemoryStream () )
                {
                    // Gán Stream mới cho Response.Body để ghi dữ liệu phản hồi
                    context.Response.Body = responseBody;

                    // Tiếp tục xử lý yêu cầu
                    await _next (context);

                    // Đọc dữ liệu phản hồi từ Stream mới
                    var response = await FormatResponse (context.Response);

                    // Ghi dữ liệu phản hồi đã được định dạng vào Response.Body ban đầu
                    await context.Response.Body.WriteAsync (response);
                }
            }
            catch ( Exception ex )
            {
                throw;
            }
            finally
            {
                // Khôi phục Response.Body ban đầu
                context.Response.Body = originalBodyStream;
            }
        }
        private async Task<byte[]> FormatResponse(HttpResponse response)
        {
            // Đọc dữ liệu phản hồi từ Response.Body
            response.Body.Seek (0, SeekOrigin.Begin);
            var responseBody = await new StreamReader (response.Body).ReadToEndAsync ();

            // Kiểm tra xem dữ liệu phản hồi có thuộc loại mô hình gốc hay không
            if ( response.StatusCode == 200 && IsModelType (response) )
            {
                // Chuyển đổi mô hình thành mô hình response
                var model = JsonConvert.DeserializeObject (responseBody);
                var responseModel = new ApiResponseModel (model);

                // Chuyển đổi mô hình response thành chuỗi JSON
                var jsonResponse = JsonConvert.SerializeObject (responseModel);

                // Chuyển đổi chuỗi JSON thành dạng byte[]
                return Encoding.UTF8.GetBytes (jsonResponse);
            }

            // Trả về dữ liệu phản hồi ban đầu nếu không cần chuyển đổi
            return Encoding.UTF8.GetBytes (responseBody);
        }

        private bool IsModelType(HttpResponse response)
        {
            // Kiểm tra xem ContentType của Response có phù hợp với loại mô hình gốc hay không
            return response.ContentType.Contains ("application/json") || response.ContentType.Contains ("application/xml");
        }

        public class ApiResponseModel
        {
            public bool? Success { get; set; }
            public string? Message { get; set; }
            public object Data { get; set; }

            public ApiResponseModel(object data)
            {
                Success = true;
                Message = "Success";
                Data = data;
            }
        }
    }
}
