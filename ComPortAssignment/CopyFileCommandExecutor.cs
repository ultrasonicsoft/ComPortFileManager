using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace ComPortAssignment
{
    internal class CopyFileCommandExecutor
    {
        private string source;
        private string destination;
        private string newFileName;
        public CopyFileCommandExecutor()
        {
            source = ConfigurationManager.AppSettings["Source"];
            destination = ConfigurationManager.AppSettings["Destination"];
            newFileName = ConfigurationManager.AppSettings["NewFileName"];
        }

        internal bool CopyFile(string fileName)
        {
            bool isFileCopied = false;

            try
            {
                var fullSourceFileName = Path.Combine(source, fileName);

                if(File.Exists(fullSourceFileName)==false)
                {
                    System.Windows.Forms.MessageBox.Show(fullSourceFileName + " file does not exists on disk");
                    return false;
                }

                var fullDestinationFileName = Path.Combine(destination, newFileName);

                File.Copy(fullSourceFileName, fullDestinationFileName, true);

                isFileCopied = File.Exists(fullDestinationFileName);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
            return isFileCopied;
        }
    }
}
