using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
//using System.Windows.Forms;

namespace Et075.Model
{
    /// <summary>
    /// Provides interactions with Windows printing subsytem
    /// </summary>
    public static class PrintingProvider
    {
        private static LocalPrintServer _localPrintServer;
        private static PrintQueueCollection _localPrinterCollection;
        private static List<string> _printerNamesList;

        static PrintingProvider()
        {
            // Initialize local print server
            _localPrintServer = new LocalPrintServer();

            // Provide enumeration flags to obtain local printers and connected network printers
            EnumeratedPrintQueueTypes[] _enumerationFlags = { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections };
            // Obtain print queues from server
            _localPrinterCollection = _localPrintServer.GetPrintQueues(_enumerationFlags);
            if (_localPrinterCollection.Count() == 0)
                throw (new Exception("Cannot find system printers. You must have at least one printer installed."));
            // Fill _printerNamesList
            _printerNamesList = new List<string>();
            foreach (PrintQueue queue in _localPrinterCollection)
                _printerNamesList.Add(queue.FullName);
        } //end:PrintingProvider

        /// <summary>
        /// Returns list with system printers' names
        /// </summary>
        public static List<string> SystemPrintersNamesList
        {
            get { return _printerNamesList; }
        }//end:SystemPrintersNamesLis

        /// <summary>
        /// Returns the name of the default system printer
        /// </summary>
        public static string DefaultPrinterName
        {
            get { return _localPrintServer.DefaultPrintQueue.FullName; }
        }//end:DefaultPrinterName

        /// <summary>
        /// Prints provided Canvas object to printer printerName with printTaskName shown in the print queue.
        /// Page size for printing is taken from Constants.a4MediaSize.
        /// </summary>
        public static void PrintDocument(System.Drawing.Printing.PrintDocument doc, string printerName)
        {
            doc.PrinterSettings.PrinterName = printerName;
            if (!doc.PrinterSettings.IsValid)
                throw (new MissingMemberException("Could not access printer \"" + printerName + "\"!"));
            doc.DocumentName = "Et075 Results";
            doc.Print();
        }//end:PrintPage

    }//end:PrintingProvider
}
