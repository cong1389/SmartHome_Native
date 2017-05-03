namespace SmartHome.Abstract
{
    public abstract class Response<T> where T : class
    {
        //public bool Success { get; set; }

        public T data { get; set; }

       // public string Error { get; set; }
    }
}
