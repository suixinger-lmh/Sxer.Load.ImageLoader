using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sxer.Load.ImageLoader
{
    /// <summary>
    /// 图片资源加载管理
    /// 使用UnityWebRequest加载图片  支持Http，ftp，file协议
    /// Texture2D.LoadImage(bytes)仅支持png和jpg
    /// 无论本地还是网络 使用UnityWebRequestTexture 存在无法识别图片类型问题(改文件后缀造成)
    /// 
    /// </summary>
    public partial class ImageLoadManager
    {
        UnityWebRequest m_unityWebRequest;

        public Texture tempTex;
        public IEnumerator WWWLoadImage_Test(string path)
        {
            m_unityWebRequest = UnityWebRequestTexture.GetTexture(path);
            yield return m_unityWebRequest.SendWebRequest();

            //while (!m_unityWebRequest.isDone)
            {
                Debug.Log("加..");
                if (!m_unityWebRequest.isNetworkError && !m_unityWebRequest.isHttpError)
                {
                    tempTex = ((DownloadHandlerTexture)m_unityWebRequest.downloadHandler).texture;
                    Debug.Log(m_unityWebRequest.downloadHandler.data.Length);
                    string temp = m_unityWebRequest.downloadHandler.data[0].ToString() + m_unityWebRequest.downloadHandler.data[1].ToString();
                    Debug.Log(m_unityWebRequest.downloadHandler.data[0]);
                    Debug.Log("加载完成");
                    Debug.Log(temp);
                    Texture2D te = new Texture2D(100, 100);
                    te.LoadImage(m_unityWebRequest.downloadHandler.data);
                    tempTex = te;
                }
            }
        }

        //public IEnumerator WWWLoadImage_Texture2D(string path)
        //{
        //    m_unityWebRequest = UnityWebRequestTexture.GetTexture(path);
        //    yield return m_unityWebRequest.SendWebRequest();
        //    if (!m_unityWebRequest.isNetworkError && !m_unityWebRequest.isHttpError)
        //    {
        //        tempTex = ((DownloadHandlerTexture)m_unityWebRequest.downloadHandler).texture;
        //        Debug.Log(m_unityWebRequest.downloadHandler.data.Length);
        //        string temp = m_unityWebRequest.downloadHandler.data[0].ToString() + m_unityWebRequest.downloadHandler.data[1].ToString();
        //        Debug.Log(m_unityWebRequest.downloadHandler.data[0]);
        //        Debug.Log("加载完成");
        //        Debug.Log(temp);
        //        Texture2D te = new Texture2D(100, 100);
        //        te.LoadImage(m_unityWebRequest.downloadHandler.data);
        //        tempTex = te;
        //    }
        //}


        public static IEnumerator WWWLoadImage_Texture2D(string path, System.Action loadedDo)
        {
            _resMap[path].m_State = ImageResLoadState.Loading;
            //Debug.Log(path);
            UnityWebRequest m_unityWebRequest = UnityWebRequestTexture.GetTexture(path);
            yield return m_unityWebRequest.SendWebRequest();
            if (!m_unityWebRequest.isNetworkError && !m_unityWebRequest.isHttpError)
            {
                _resMap[path].resTexture2D = ((DownloadHandlerTexture)m_unityWebRequest.downloadHandler).texture;
                _resMap[path].m_State = ImageResLoadState.Loaded;
                if (loadedDo != null)
                    loadedDo();
            }
            else
            {
                _resMap[path].resTexture2D = null;
                _resMap[path].m_State = ImageResLoadState.Loaded;
                if (loadedDo != null)
                    loadedDo();
            }
        }
        public IEnumerator WWWLoadImage_Texture2D(ImageRes res)
        {
            m_unityWebRequest = UnityWebRequestTexture.GetTexture(res.imagePath);
            yield return m_unityWebRequest.SendWebRequest();
            if (!m_unityWebRequest.isNetworkError && !m_unityWebRequest.isHttpError)
            {
                res.resTexture2D = ((DownloadHandlerTexture)m_unityWebRequest.downloadHandler).texture;
            }
        }

        private IEnumerator UnityWWWLoadImage_GetTexture(string path)
        {
            m_unityWebRequest = UnityWebRequestTexture.GetTexture(path);
            yield return m_unityWebRequest.SendWebRequest();
            if (!m_unityWebRequest.isNetworkError && !m_unityWebRequest.isHttpError)
            {
                tempTex = ((DownloadHandlerTexture)m_unityWebRequest.downloadHandler).texture;
                Debug.Log(m_unityWebRequest.downloadHandler.data.Length);
                string temp = m_unityWebRequest.downloadHandler.data[0].ToString() + m_unityWebRequest.downloadHandler.data[1].ToString();
                Debug.Log(m_unityWebRequest.downloadHandler.data[0]);
                Debug.Log("加载完成");
                Debug.Log(temp);
                Texture2D te = new Texture2D(100, 100);
                te.LoadImage(m_unityWebRequest.downloadHandler.data);
                tempTex = te;
            }
            else
            {
                Debug.LogError(m_unityWebRequest.error);
            }
        }

        private IEnumerator UnityWWWLoadImage_GetByte(string path, System.Action<byte[]> afterLoad)
        {
            m_unityWebRequest = UnityWebRequest.Get(path);
            yield return m_unityWebRequest.SendWebRequest();
            if (!m_unityWebRequest.isNetworkError && !m_unityWebRequest.isHttpError)
            {
                if (afterLoad != null)
                    afterLoad(m_unityWebRequest.downloadHandler.data);
                //string temp = m_unityWebRequest.downloadHandler.data[0].ToString() + m_unityWebRequest.downloadHandler.data[1].ToString();
                //Debug.Log(temp);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError(m_unityWebRequest.error);
#endif
                if (afterLoad != null)
                    afterLoad(null);
            }
        }



        //按资源表加载
        //按绝对路径加载

        //网络加载
        //IO加载





        //单张加载
        //批量加载

        //阻塞，异步
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