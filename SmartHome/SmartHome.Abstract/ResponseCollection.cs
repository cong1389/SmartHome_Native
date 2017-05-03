using System.Collections.Generic;

namespace SmartHome.Abstract
{
    public abstract class ResponseCollection<T> where T : class
    {
        //public bool Success { get; set; }
        public List<T> data { get; set; }
        //public string Error { get; set; }
    }
}
