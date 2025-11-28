using Naninovel;


namespace NaniFutures
{
    public class ScriptCompilerFuture : ScriptCompiler
    {
        protected override GenericLineCompiler GenericLineCompiler { get; }
            = new GenericLineCompilerFuture();
    }
}