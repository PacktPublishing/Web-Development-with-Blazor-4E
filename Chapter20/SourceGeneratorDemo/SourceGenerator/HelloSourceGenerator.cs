using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace SourceGenerator;

[Generator]
public sealed class HelloSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static ctx =>
        {
            var source = """
            namespace BlazorWebAssembly;

            public class GeneratedService
            {
                public string GetHello()
                {
                    return "Hello from generated code";
                }
            }
            """;

            ctx.AddSource("GeneratedService.g.cs", SourceText.From(source, Encoding.UTF8));
        });
    }
}
