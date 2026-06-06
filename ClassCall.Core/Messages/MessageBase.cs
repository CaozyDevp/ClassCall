using Newtonsoft.Json;

namespace ClassCall.Core.Messages
{
    public class MessageBase<T>
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static T Parse(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static bool TryParse(string json, out T message)
        {
            try
            {
                message = Parse(json);
                if (message == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                message = default;
                return false;
            }
        }
    }
}
