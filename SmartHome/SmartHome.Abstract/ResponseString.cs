namespace SmartHome.Abstract
{
    public abstract class ResponseString<T> where T : class
    {
        public bool Success { get; set; }

        public string data { get; set; }

       // public string Error { get; set; }
    }
}
