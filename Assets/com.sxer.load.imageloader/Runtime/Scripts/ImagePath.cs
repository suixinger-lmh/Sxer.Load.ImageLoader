using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sxer.Load.ImageLoader
{
    public class ImagePath
    {
        public string realPath;

        public ImageLoadManager.PathType pathType = ImageLoadManager.PathType.NullPath;

        public ImagePath()
        {
            realPath = string.Empty;
        }
        public ImagePath(string path)
        {
            realPath = path;
        }

        public ImagePath(string path, ImageLoadManager.PathType imagePathType)
        {
            realPath = path;
            pathType = imagePathType;
        }
    }
}

