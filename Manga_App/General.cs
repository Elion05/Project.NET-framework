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

        public static  string ApiUrl = "https://localhost:7280/api/";

        internal static MangaUser User = null;
        internal static string UserId = "";


        public static long LocalIdCounter = -1;

    }
}
