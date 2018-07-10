
namespace UserManagement.LocalObjects
{
    public class GenericActionResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public GenericActionResult() { }

        public GenericActionResult(string message)
        {
            Message = message;
        }

        public GenericActionResult(bool success, string message) : this(message)
        {
            Success = success;
        }

        public GenericActionResult(bool success, string message, T data)
            : this(success, message)
        {
            Data = data;
        }

        public static GenericActionResult<T> FromObject(T obj)
        {
            return new GenericActionResult<T>(true, "", obj);
        }
    }
}
