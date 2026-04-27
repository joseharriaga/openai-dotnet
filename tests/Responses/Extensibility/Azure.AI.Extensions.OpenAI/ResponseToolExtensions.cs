using OpenAI.Responses;
using System;
using System.ClientModel.Primitives;

namespace Azure.AI.Extensions.OpenAI;

public static class ResponseToolExtensions
{
    public static ResponseTool AsKnownAzureTool(this ResponseTool tool)
    {
        ArgumentNullException.ThrowIfNull(tool);

        if (tool.Kind == ResponseToolKind.BingGrounding() && tool is not BingGroundingTool)
        {
            return ModelReaderWriter.Read<BingGroundingTool>(
                tool.Patch.GetJson("$."u8),
                ModelSerializationExtensions.WireOptions,
                AzureAIExtensionsOpenAIContext.Default);
        }

        return tool;
    }
}
