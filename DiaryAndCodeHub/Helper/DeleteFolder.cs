using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DiaryAndCodeHub.Helper
{
    public static class DeleteFolder
    {
        public static void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);
            try
            {

                foreach (FileInfo fi in dir.GetFiles())
                {
                    try
                    {
                        fi.Delete();
                    }
                    catch (Exception) { } // Ignore all exceptions
                }
            }
            catch (Exception) { }
            try
            {
                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    ClearFolder(di.FullName);
                    try
                    {
                        di.Delete();
                    }
                    catch (Exception) { } // Ignore all exceptions
                }
            }
            catch (Exception) { }
        }

    }
}