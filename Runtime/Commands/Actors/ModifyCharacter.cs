using UnityEngine;

namespace Naninovel.Commands
{
    [Doc(@"
Modifies a [character actor](/guide/characters).",
        null,
        @"
; Shows character with ID 'Sora' with a default appearance.
@char Sora",
        @"
; Same as above, but sets appearance to 'Happy'.
@char Sora.Happy",
        @"
; Same as above, but additionally positions the character 45% away 
; from the left border of the scene and 10% away from the bottom border; 
; also makes it look to the left.
@char Sora.Happy look:left pos:45,10",
        @"
; Make Sora appear at the bottom-center and in front of Felix.
@char Sora pos:50,0,-1
@char Felix pos:,,0",
        @"
; Tint all visible characters on scene.
@char * tint:#ffdc22",
        @"
; Link 'Bubble' printer to 'Sora' and 'Felix' characters, so it will be used when they are the author.
@char Sora,Felix printer:Bubble",
        @"
; Remove the runtime-set linked printer for 'Sora', reverting to the configured default.
@char Sora printer:"
    )]
    [Alias("char"), ActorsGroup, Icon("User")]
    [ActorContext(CharactersConfiguration.DefaultPathPrefix, paramId: nameof(Id))]
    [ConstantContext("Poses/Characters/{:Id??:IdAndAppearance[0]}+Poses/Characters/*", paramId: nameof(Pose))]
    public class ModifyCharacterFuture : ModifyCharacter
    {
        [Doc("ID of the character to modify (specify `*` to affect all visible characters) and an appearance (or [pose](/guide/characters#poses)) to set. " +
             "When appearance is not specified, will use either a `Default` (is exists) or a random one. " +
             "When `printer` parameter is specified, multiple character IDs can be provided (comma-separated), but appearance is not allowed.")]
        [Alias(NamelessParameterAlias), RequiredParameter, ActorContext(CharactersConfiguration.DefaultPathPrefix, 0), AppearanceContext(1)]
        public NamedStringListParameter IdAndAppearance;

        [Alias("printer"), ActorContext(TextPrintersConfiguration.DefaultPathPrefix)]
        public StringParameter PrinterId;

        // Disable preload when PrinterId is assigned since we're in printer-linking mode and won't be modifying actors.
        protected override bool AllowPreload => !IdAndAppearance.DynamicValue && !Assigned(PrinterId);
        protected override string AssignedId => base.AssignedId ?? IdAndAppearance[0]?.Name;
        protected override string AlternativeAppearance => Assigned(PrinterId) ? null : IdAndAppearance[0]?.NamedValue;

        /// <summary>
        /// Whether the command is in printer-linking mode (only linking printer to characters, no actor modifications).
        /// </summary>
        protected virtual bool IsPrinterLinkingMode => Assigned(PrinterId);

        public override async UniTask Execute (ExecutionContext ctx)
        {
            // When printer parameter is specified, handle printer linking only
            if (IsPrinterLinkingMode)
            {
                // Validate that no appearance is specified when printer is set
                if (IdAndAppearance != null)
                {
                    foreach (var item in IdAndAppearance)
                    {
                        if (!string.IsNullOrEmpty(item?.NamedValue))
                            throw Fail($"Appearance '{item.NamedValue}' is not allowed when 'printer' parameter is specified. Use '@char CharId printer:PrinterId' without appearance.");
                    }
                }

                await HandlePrinterLinking();
                return;
            }

            await base.Execute(ctx);
        }

        /// <summary>
        /// Handles linking printer to one or more characters using the NamedStringListParameter.
        /// </summary>
        protected virtual UniTask HandlePrinterLinking ()
        {
            if (IdAndAppearance == null) return UniTask.CompletedTask;

            var printerId = PrinterId?.Value;

            foreach (var item in IdAndAppearance)
            {
                var characterId = item?.Name;
                if (string.IsNullOrEmpty(characterId)) continue;

                if (string.IsNullOrEmpty(printerId))
                {
                    // Empty printer value - remove the linked printer for this character
                    ActorManager.RemoveLinkedPrinterFor(characterId);
                }
                else
                {
                    // Link the specified printer to this character
                    ActorManager.SetLinkedPrinterFor(characterId, printerId);
                }
            }

            return UniTask.CompletedTask;
        }
    }
}
