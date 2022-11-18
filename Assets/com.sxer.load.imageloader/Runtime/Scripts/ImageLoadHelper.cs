using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sxer.Load.ImageLoader
{
    public enum HelperState
    {
        None,
        Ready,
        Loading,
        Finish
    }
    /// <summary>
    /// 图片加载批量管理工具
    /// 批量图片路径
    /// 【】
    /// 
    /// 准备完成的批量图片
    /// </summary>
    public class ImageLoadHelper :MonoBehaviour
    {
        //内部：协程管理
        //外部：判断加载状态
        //当前路径数量，无效路径，已加载路径，需要加载路径

        //批量管理
        //协程管理
        //
        //
        public string _tag;

        public List<string> _imageStrPaths;
        public List<ImagePath> _imagePaths;

        public HelperState _helperState = HelperState.None;
        public float _loadProcess = 0;

        public Texture2D[] _imageTexs;
        bool isLoadComplate = false;
        ImageLoadManager _manager;

       

        
        Action<Texture2D[]> preparedDO;
        
        
        /// <summary>
        /// 初始准备，拿到路径，和回调事件
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="imagePaths"></param>
        /// <param name="afterload"></param>
        public void InitHelper(ImageLoadManager manager, List<string> imagePaths, Action<Texture2D[]> afterload)
        {
            _tag = transform.name;
            _manager = manager;
            _imageStrPaths = imagePaths;
            preparedDO = afterload;

            _loadProcess = 0;
            //初始化
            //当preparedCount = needwaitcount+ needloadcount时，本次资源准备完成
            preparedCount = 0;//当前资源准备完成数量
           
#if UNITY_EDITOR
            //基础信息
            DebugLoadInfo();
#endif

            _helperState = HelperState.Ready;
        }

        /// <summary>
        /// 进行资源准备
        /// </summary>
        public void ResourceLoad()
        {
            _helperState = HelperState.Loading;
            
            BeforeLoadCheck();//检测所有路径状态
            
            //准备资源
            StartCoroutine(PrepareAllRes());

            StartCoroutine(WaitPrepareImage());
        }



        void DebugLoadInfo()
        {
            int allPath = _imageStrPaths.Count;
            int nullPath = _imageStrPaths.FindAll(p => string.IsNullOrEmpty(p)).Count;
            Debug.Log( string.Format("当前批次Tag:{0}\r\n当前路径总量：{1}\r\n空路径数：{2}\r\n", _tag,allPath, nullPath));
        }

        void BeforeLoadCheck()
        {
            _imagePaths = new List<ImagePath>();
            foreach(var x in _imageStrPaths)
            {
                _imagePaths.Add(ImageLoadManager.CheckPath(x));
            }

            //本次需要提取数量
            needWaitCount = _imagePaths.FindAll(p => p.pathType == ImageLoadManager.PathType.DoWaitPath).Count + _imagePaths.FindAll(p => p.pathType == ImageLoadManager.PathType.DoGetPath).Count;
            //本次需要新加载数量
            needLoadCount = _imagePaths.FindAll(p => p.pathType == ImageLoadManager.PathType.NewPath).Count;

#if UNITY_EDITOR
            Debug.Log(string.Format("提取数量：{0}\r\n加载数量:{1}\r\n",needWaitCount,needLoadCount));
#endif
        }


        public int preparedCount;// = needloadcount+needwaitcount;
        public int needLoadCount;
        public int needWaitCount;

        IEnumerator PrepareAllRes()
        {
            for(int i = 0; i < _imagePaths.Count; i++)
            {
                int tempInt = i;
                switch (_imagePaths[tempInt].pathType)
                {
                    case ImageLoadManager.PathType.NewPath://需要加载的
                        yield return StartCoroutine(ImageLoadManager.WWWLoadImage_Texture2D(_imagePaths[tempInt].realPath,()=> { preparedCount++; }));
                        break;
                    case ImageLoadManager.PathType.DoWaitPath://需要等待的
                        yield return StartCoroutine(ImageLoadManager.CheckFinish(_imagePaths[tempInt].realPath, () => { preparedCount++; }));
                        break;
                    case ImageLoadManager.PathType.DoGetPath://已经有的
                        preparedCount++;
                        break;
                }
            }
        }
        IEnumerator WaitPrepareImage()
        {
            while (needLoadCount + needWaitCount > preparedCount)
            {
                _loadProcess = preparedCount * 1.0f / (needLoadCount + needWaitCount);
                yield return null;
            }
            _loadProcess = preparedCount * 1.0f / (needLoadCount + needWaitCount);
            isLoadComplate = true;
            _helperState = HelperState.Finish;
            //整理图片资源
            GetAllImages();
            //图片准备完成
            DoLoadHelperFinish();
        }
        void GetAllImages()
        {
            _imageTexs = new Texture2D[_imagePaths.Count];
            for(int i = 0; i < _imagePaths.Count; i++)
            {
                if (_imagePaths[i].pathType == ImageLoadManager.PathType.NullPath)
                    _imageTexs[i] = null;
                else
                    _imageTexs[i] = _manager.GetImageRes(_imagePaths[i].realPath);
            }
        }
        void DoLoadHelperFinish()
        {
            Debug.Log("图片准备完成");
            if (preparedDO != null)
                preparedDO(_imageTexs);
        }
        public void StartLoadAction(string path)
        {
            isLoadComplate = true;
            StartCoroutine(WWWLoadImage_Texture2D(path));

            
            //while (waitLoad)//加载中
            //{
            //    Debug.Log("wait");
            //}

            //
            Debug.Log("加载完成");
        }

        public Texture2D GetTexture2D(string path)
        {
            return tempTex;
        }

        Texture2D tempTex;
        public IEnumerator WWWLoadImage_Texture2D(string path)
        {
            UnityWebRequest m_unityWebRequest = UnityWebRequestTexture.GetTexture(path);
            yield return m_unityWebRequest.SendWebRequest();
            if (!m_unityWebRequest.isNetworkError && !m_unityWebRequest.isHttpError)
            {
                tempTex = ((DownloadHandlerTexture)m_unityWebRequest.downloadHandler).texture;
                isLoadComplate = false;
            }
        }

    }
}

