using Naninovel;
using Naninovel.Syntax;
using static Naninovel.Command;
using static Naninovel.CommandParameter;


namespace NaniFutures
{
    public class ScriptCompilerFuture : ScriptCompiler
    {

        private readonly CompileErrorHandler errHandler = new();
        private readonly TextMapper textMapper = new();

        public ScriptCompilerFuture ()
        {
            // ScriptParser = new(new() {
            //     Symbols = Compiler.Symbols,
            //     Handlers = new() {
            //         ErrorHandler = errHandler,
            //         TextIdentifier = textMapper
            //     }
            // });
            // CommentLineCompiler = new();
            // LabelLineCompiler = new();
            // CommandLineCompiler = new(textMapper, errHandler);
            // GenericLineCompiler = new(textMapper, errHandler);
            // CommandCompiler = new(textMapper, errHandler, false);
        }
    }
}