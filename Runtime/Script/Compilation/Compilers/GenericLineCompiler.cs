using Naninovel;


namespace NaniFutures
{
    public class GenericLineCompilerFuture : GenericLineCompiler
    {
        protected virtual void AddAppearanceChange()
        {
            if (string.IsNullOrEmpty(AuthorId)) return;
            if (string.IsNullOrEmpty(AuthorAppearance)) return;
            AddCommand(new ModifyCharacter
            {
                IsGenericPrefix = true,
                IdAndAppearance = new List<NullableNamedString> { new NamedString(AuthorId, AuthorAppearance) },
                Wait = false,
                PlaybackSpot = Spot,
                Indent = Syntax.Indent
            });
        }
    }
}