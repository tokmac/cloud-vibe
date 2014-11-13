using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Vibe.Data.Models
{
    public interface IDownloadable
    {
        byte[] Torrent { get; set; }

        User UserShared { get; set; }

        string TypeMIME { get; set; }

        string Title { get; set; }

        int Downloads { get; set; }
    }
}
