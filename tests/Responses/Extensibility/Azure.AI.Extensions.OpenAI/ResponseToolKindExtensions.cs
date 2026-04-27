using OpenAI.Responses;

namespace Azure.AI.Extensions.OpenAI;

public static class ResponseToolKindExtensions
{
    extension(ResponseToolKind)
    {
        public static ResponseToolKind BingGrounding() => new("bing_grounding");
    }
}
