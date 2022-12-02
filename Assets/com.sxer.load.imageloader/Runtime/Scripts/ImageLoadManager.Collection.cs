using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sxer.Load.ImageLoader
{
    /// <summary>
    /// 图片资源加载管理
    /// </summary>
    public partial class ImageLoadManager
    {
        public enum PathType
        {
            NewPath,//需要加载
            DoWaitPath,//不需要加载 需要等待加载完成
            DoGetPath,//不需要加载 直接获取
            NullPath//不使用的路径
        }

        public enum LoadTimeType
        {
            DoNotLoad,//不管理
            APILoad,//通过代码启动下载
            UpdateLoad,//update轮询下载
            OnlyOneLoad//同时只开启一个下载
        }

        public LoadTimeType _loadTimeType = LoadTimeType.OnlyOneLoad;


        public Texture2D _defaultTexture = null;

        //获取路径

        //对路径进行检验（路径分三类:新路径(生成资源，并获取)，已存在无资源路径(等待资源再获取)，已存在有资源路径(只获取数据)）
        
        
        /// <summary>
        /// 资源路径和对应资源实体
        /// </summary>
        static Dictionary<string, ImageRes> _resMap = new Dictionary<string, ImageRes>();

        List<ImageRes> imageRes = new List<ImageRes>();

        /// <summary>
        /// 图片处理工具集合tag和工具实体
        /// </summary>
        Dictionary<string, ImageLoadHelper> _loadHelperMap = new Dictionary<string, ImageLoadHelper>();

        List<ImagePath> _tempImagePaths = new List<ImagePath>();
        //Dictionary<string,List<>>

        //生成对应资源类

        //开始下载

        //记录到资源类 //



        //按资源表加载
        //按绝对路径加载

        //网络加载
        //IO加载




        //路径记录表
        //public List<string> _tempPaths;

        //List<ImageRes> res;




        //单张加载
        //批量加载


        //IO加载
        //WWW加载

        //通用方式：加载字节数据，(判断图片类型)，转对应图片
        //特殊方式:WWW直接加载Image 对异常图片没有判断

        //加载效果：每张图开启一个协程去加载


        //public static IEnumerator WWWLoadSprite_native(string path, UnityAction<Sprite> action)
        //{

        //    UnityWebRequest www = UnityWebRequestTexture.GetTexture(path);

        //    yield return www.SendWebRequest();

        //    if (!www.isNetworkError && !www.isHttpError)
        //    {
        //        Texture2D texture2D = DownloadHandlerTexture.GetContent(www);
        //        if (action != null)
        //            action(Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero));
        //        //清理资源
        //        texture2D = null;
        //        www.Dispose();
        //        Resources.UnloadUnusedAssets();
        //    }
        //    else
        //    {
        //        Debug.Log(path);
        //        Debug.LogError(www.error);
        //        if (action != null)
        //            action(null);
        //        //Debug.LogError(www.error);
        //    }

        //}






    }
}