using Microsoft.AspNetCore.Mvc;

namespace AwesomePizza.Test
{
    public class Utility
    {
        public static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
