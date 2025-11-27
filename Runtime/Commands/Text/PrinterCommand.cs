namespace Naninovel.Commands
{
    public abstract class PrinterCommandFuture : PrinterCommand
    {
        protected virtual async UniTask<ITextPrinterActor> GetOrAddPrinter (AsyncToken token = default)
        {
            var printerId = default(string);

            if (string.IsNullOrEmpty(AssignedPrinterId) && !string.IsNullOrEmpty(AssignedAuthorId))
            {
                // First, check for a runtime-set linked printer (set via @printer command with `char` parameter)
                printerId = Characters.GetLinkedPrinterFor(AssignedAuthorId);
                
                // Fall back to the hardcoded configuration if no runtime-set value exists
                if (string.IsNullOrEmpty(printerId))
                    printerId = Characters.Configuration.GetMetadataOrDefault(AssignedAuthorId).LinkedPrinter;
            }

            if (string.IsNullOrEmpty(printerId))
                printerId = AssignedPrinterId;

            var printer = await Printers.GetOrAddActor(printerId ?? Printers.DefaultPrinterId);
            token.ThrowIfCanceled();
            return printer;
        }
    }
}
