using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TK1.Utility;

namespace TK1.Bizz.Sync
{
    public class FileLoader
    {
        #region PRIVATE MEMBERS
        private List<string> filesToLoad;
        private List<string> loadedFiles;
        private List<string> errorFiles;

        #endregion

        #region PUBLIC PROPERTIES
        public string CurrentFile
        {
            get
            {
                if (filesToLoad == null) 
                    return null; 
                else 
                    return filesToLoad.FirstOrDefault();
            }
        }
        public string DestinationDirectory { get; set; }
        public string ErrorDirectory { get; set; }
        public string SourceDirectory { get; set; }

        #endregion

        public FileLoader(string sourceDirectory, string destinationDirectory, string errorDirectory)
        {
            this.DestinationDirectory = destinationDirectory;
            this.ErrorDirectory = errorDirectory;
            this.SourceDirectory = sourceDirectory;
        }



        public void LoadFiles(string fileFilter)
        {
            filesToLoad = FileHelper.GetFiles(SourceDirectory, fileFilter);
            errorFiles = new List<string>();
            loadedFiles = new List<string>();
        }

        public bool ReadFile()
        {
            bool result = false;
            if (filesToLoad != null & loadedFiles != null & errorFiles != null)
            {
                result = filesToLoad.Count > 0;
            }

            return result;
        }

    }
}
