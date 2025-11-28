// This file is not being used and is left here for reference purposes only.


// using Naninovel;

// [InitializeAtRuntime(@override: typeof(ICharacterManager))]
// public class ICharacterManagerFuture : ICharacterManager
// {
//     /// <summary>
//     /// Attempts to retrieve the runtime-set linked printer ID for a character with the specified ID.
//     /// Will return null when character is not found or doesn't have a runtime-set linked printer.
//     /// </summary>
//     [CanBeNull] string GetLinkedPrinterFor(string characterId);
//     /// <summary>
//     /// Sets the linked printer for a character at runtime.
//     /// This overrides the LinkedPrinter configured in metadata.
//     /// </summary>
//     void SetLinkedPrinterFor(string characterId, string printerId);
//     /// <summary>
//     /// Removes the runtime-set linked printer for a character with the specified ID,
//     /// reverting to the default configuration.
//     /// </summary>
//     void RemoveLinkedPrinterFor(string characterId);
// }