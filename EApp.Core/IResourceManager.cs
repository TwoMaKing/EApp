using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;

namespace EApp.Core
{
    /// <summary>
    /// The resource manager to get resource e.g. text, image, stream, object.
    /// Provides convenient access to culture-specific resources at run time.
    /// </summary>
    public interface IResourceManager
    {
        /// <summary>
        /// Returns the value of the specified System.Object resource.
        /// </summary>
        object GetObject(string name);

        /// <summary>
        /// Gets the value of the System.Object resource localized for the specified culture.
        /// </summary>
        object GetObject(string name, CultureInfo culture);

        /// <summary>
        /// Returns an System.IO.UnmanagedMemoryStream object from the specified resource
        /// </summary>
        UnmanagedMemoryStream GetStream(string name);

        /// <summary>
        ///  Returns an System.IO.UnmanagedMemoryStream object from the specified resource using the specified culture.
        /// </summary>
        UnmanagedMemoryStream GetStream(string name, CultureInfo culture);

        /// <summary>
        /// Returns the value of the specified System.Drawing.Imaging resource.
        /// </summary>
        Image GetImage(string name);

        /// <summary>
        /// Gets the value of the System.Drawing.Imaging resource localized for the specified
        /// culture.
        /// </summary>
        Image GetImage(string name, CultureInfo culture);

        /// <summary>
        /// Returns the value of the specified System.String resource.
        /// </summary>
        string GetString(string name);


        /// <summary>
        /// Gets the value of the System.String resource localized for the specified
        /// culture.
        /// </summary>
        string GetString(string name, CultureInfo culture);
    }
}
