using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel;
using Naninovel.Syntax;
using NaniFutures.Commands;

namespace NaniFutures
{
    public class GenericLineCompilerFuture : GenericLineCompiler
    {

        public GenericLineCompilerFuture(ITextIdentifier identifier, IErrorHandler errorHandler) 
            : base(identifier, errorHandler)
        {
        }
        
        protected override void AddAppearanceChange()
        {
            if (string.IsNullOrEmpty(AuthorId)) return;
            if (string.IsNullOrEmpty(AuthorAppearance)) return;
            AddCommand(new ModifyCharacterFuture
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