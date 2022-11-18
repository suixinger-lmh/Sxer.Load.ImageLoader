using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sxer.Load.ImageLoader
{
    public enum ImageType
    {
        PNG = 13780,
        JPG = 255216,
        BMP = 6677,
        GIF = 7173
        /*
         JPG = 255216,
    GIF = 7173,
    PNG = 13780,
    SWF = 6787,
    RAR = 8297,
    ZIP = 8075,
    _7Z = 55122,
    VALIDFILE=9999999
    // 255216 jpg; 

    // 7173 gif; 

    // 6677 bmp, 

    // 13780 png; 

    // 6787 swf 

    // 7790 exe dll, 

    // 8297 rar 

    // 8075 zip 

    // 55122 7z 

    // 6063 xml 

    // 6033 html 

    // 239187 aspx 

    // 117115 cs 

    // 119105 js 

    // 102100 txt 

    // 255254 sql  

         */
    }



    public enum ImageLoadType
    {
        Net,
        Local
    }

    public enum ImageResLoadState
    {
        Null,//初始状态
        Loading,//加载中
        Loaded//已加载
    }

    public class ImageRes
    {
        //资源路径
        public string imagePath;
        public Texture2D resTexture2D;
        public byte[] resByte;

        //资源图片类型
        ImageType imageType;
        //资源图片尺寸
        public Vector2 imageSize;

        public ImageResLoadState m_State = ImageResLoadState.Null;
 

        //回调事件
        UnityEngine.Events.UnityAction<ImageRes> imageLoadComplete;

        public ImageType m_ImageType { get => imageType; set => imageType = value; }

        //资源字节流
        //资源纹理
        // public Texture
        //转字节
        //

    }
}
