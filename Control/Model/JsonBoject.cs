using Newtonsoft.Json;

namespace Control.Model
{
    public static class JsonBoject
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
