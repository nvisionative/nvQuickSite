// Copyright (c) 2016-2020 nvisionative, Inc.
//
// This file is part of nvQuickSite.
//
// nvQuickSite is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// nvQuickSite is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with nvQuickSite.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

using nvQuickSite.Exceptions;
using Serilog;

/// <summary>
/// Controller for extracting a package.
/// </summary>
public class PackageExtractorController
{
    /// <summary>
    /// Action to be executed when the progress is updated.
    /// </summary>
    public event Action<string> ProgressUpdated;

    /// <summary>
    /// Action to be executed when the progress value is updated.
    /// </summary>
    public event Action<int> ProgressValueUpdated;

    /// <summary>
    /// Extracts a package.
    /// </summary>
    /// <param name="openPath">Open Path.</param>
    /// <param name="savePath">Save Path.</param>
    /// <exception cref="ReadAndExtractException">Read and Extract Exception.</exception>
    public void ExtractPackage(string openPath, string savePath)
    {
        try
        {
            Log.Logger.Information("Extracting package");

            using (var zip = ZipFile.OpenRead(openPath))
            {
                long totalSize = zip.Entries.Sum(entry => entry.Length);

                long progressSize = 0;

                foreach (var entry in zip.Entries)
                {
                    var entryPath = Path.Combine(savePath, entry.FullName);

                    if (string.IsNullOrEmpty(entry.Name)) // Directory
                    {
                        Directory.CreateDirectory(entryPath);
                    }
                    else // File
                    {
                        var directoryPath = Path.GetDirectoryName(entryPath);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        this.ExtractEntryWithProgress(entry, entryPath, ref progressSize, totalSize);
                    }
                }
            }

            this.ProgressUpdated?.Invoke("Congratulations! Your new site is now ready to visit!");
        }
        catch (Exception ex)
        {
            var message = "Error attempting to read and extract the package";
            Log.Error(ex, message);
            throw new ReadAndExtractException(message, ex) { Source = "Read And Extract Package" };
        }

        Log.Logger.Information("Extracted package from {openPath} to {savePath}", openPath, savePath);
    }

    private void ExtractEntryWithProgress(ZipArchiveEntry entry, string destinationPath, ref long progressSize, long totalSize)
    {
        this.ProgressUpdated?.Invoke("Copying: " + entry.FullName);

        using (var inputStream = entry.Open())
        using (var outputStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
        {
            var buffer = new byte[81920];
            int bytesRead;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
                progressSize += bytesRead;

                int progressPercentage = (int)((progressSize * 100) / totalSize);
                this.ProgressValueUpdated?.Invoke(progressPercentage);
            }
        }
    }
}
