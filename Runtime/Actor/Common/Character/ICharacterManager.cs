// This file is not being used and is left here for reference purposes only.

using JetBrains.Annotations;
using Naninovel;

namespace NaniFutures
{
    public static class ICharacterManagerExtensions
    {
        /// <summary>
        /// Attempts to retrieve the runtime-set linked printer ID for a character with the specified ID.
        /// Will return null when character is not found or doesn't have a runtime-set linked printer.
        /// </summary>
        [CanBeNull]
        static string GetLinkedPrinterFor(this ICharacterManager i, string characterId)
        {
            if (string.IsNullOrEmpty(characterId)) return null;
            return linkedPrinterByCharId.TryGetValue(characterId, out var printerId) ? printerId : null;
        }
        /// <summary>
        /// Sets the linked printer for a character at runtime.
        /// This overrides the LinkedPrinter configured in metadata.
        /// </summary>
        static void SetLinkedPrinterFor(this ICharacterManager i, string characterId, string printerId)
        {
            if (string.IsNullOrEmpty(characterId))
            {
                Engine.Warn("Failed to set linked printer: character ID is null or empty.");
                return;
            }

            if (string.IsNullOrEmpty(printerId))
            {
                RemoveLinkedPrinterFor(characterId);
                return;
            }

            linkedPrinterByCharId[characterId] = printerId;
        }
        /// <summary>
        /// Removes the runtime-set linked printer for a character with the specified ID,
        /// reverting to the default configuration.
        /// </summary>
        static void RemoveLinkedPrinterFor(this ICharacterManager i, string characterId)
        {
            if (!string.IsNullOrEmpty(characterId))
                linkedPrinterByCharId.Remove(characterId);
        }
    }
}