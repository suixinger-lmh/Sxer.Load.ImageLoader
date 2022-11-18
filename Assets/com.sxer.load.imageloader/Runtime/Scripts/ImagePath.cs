using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Sxer.Load.ImageLoader.ImageLoadManager;

namespace Sxer.Load.ImageLoader
{
    public class ImagePath
    {
        public string realPath;

        public PathType pathType = ImageLoadManager.PathType.NullPath;

        public ImagePath()
        {
            realPath = string.Empty;
        }
        public ImagePath(string path)
        {
            realPath = path;
        }

        public ImagePath(string path, PathType imagePathType)
        {
            realPath = path;
            pathType = imagePathType;
        }
    }
}

