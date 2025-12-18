using MangaBook_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga_App
{
    internal static class General
    {


        internal static readonly string ApiUrl = "http://localhost:5128/api/";

        internal static MangaUser User = null;
        internal static string UserId = "";

        internal static long LocalIdCounter = -1;
    }
}
