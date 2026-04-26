using OpenAI.Responses;

namespace Azure.AI.Extensions.OpenAI;

public static class ResponseToolKindExtensions
{
    public static ResponseToolKind BingGrounding() => new("bing_grounding");
}
