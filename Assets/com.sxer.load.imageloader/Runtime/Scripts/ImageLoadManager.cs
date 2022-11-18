using System;
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

        //checkpath放在生成工具之前，每个批次的加载顺序就写死了，等待的开启加载只能等加载完的批次完成。
        //避免加载顺序写死，只能在启动加载时，再进行路径check
        #region 对外接口
        public void LoadImage(string path, Action<Texture2D[]> loadAction = null)
        {
            //创建加载工具
            CreateLoadHelper(new List<string>() { path}, loadAction);
        }
        public void LoadImage(string[] path, Action<Texture2D[]> loadAction = null)
        {
            //创建加载工具
            CreateLoadHelper(new List<string>(path), loadAction);
        }
        //图片路径
        public void LoadImage(List<string> path, Action<Texture2D[]> loadAction = null)
        {
            //创建加载工具
            CreateLoadHelper(path, loadAction);
        }

        /// <summary>
        /// 提供通过tag获取图片加载工具，可用工具处理资源
        /// </summary>
        public void LoadImageWithTag()
        {

        }

        #endregion


        public Texture2D GetImageRes(string path)
        {
            Texture2D tempTex;
            Debug.LogError(path);
            if (_resMap.ContainsKey(path))
            {
                Debug.LogError("o");
                // tempTex =;
                Debug.LogError(_resMap[path].m_State);
                Debug.LogError(_resMap[path].resTexture2D.width);

                return _resMap[path].resTexture2D;
            }
            else
                return null;
        }



        //生成对应资源类

        //开始下载

        //记录到资源类 //



        //按资源表加载
        //按绝对路径加载

        //网络加载
        //IO加载




       




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