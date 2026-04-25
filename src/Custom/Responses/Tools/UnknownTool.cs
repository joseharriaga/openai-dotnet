using Microsoft.TypeSpec.Generator.Customizations;

namespace OpenAI.Responses;

[CodeGenType("UnknownTool")]
public partial class UnknownTool
{
    public UnknownTool(ResponseToolKind kind)
        : base(kind)
    {
    }
}
