using Sxer.Load.ImageLoader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public RawImage rawImage;
    // Start is called before the first frame update
    void Start()
    {
        string[] tempstr = new string[] {
            @"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg",
            @"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg",
            @"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg",
            @"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg",
            @"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg",
            @"",
            @"C:\Users\DS\Desktop\郊庙歌辞 晋昭德成\郊庙歌辞 晋昭德成.jpg",
            @"C:\Users\DS\Desktop\郊庙歌辞 晋昭德成\郊庙歌辞 晋昭德成 - 副本.bmp"

        };

        string[] tempstr1 = new string[] {
            @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （1）.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （2）.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （3）.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （4）.jpg",
          @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （5）.jpg",
        @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （6）.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （7）.jpg",
          @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （8）.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （9）.jpg",
            @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （10）.jpg"
        };

        string[] tempstr2 = new string[] {
            @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （1）.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （2）.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （3）.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （4）.jpg",
          @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （5）.jpg",
        @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （6）.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （7）.jpg",
          @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （8）.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （9）.jpg",
            @"D:\SVN_CheckOut\ProjectRoot\ttesttt\Assets\StreamingAssets\屏保\1 （10）.jpg"
        };

        string[] tempstr3 = new string[] {
            @"D:\SVN_CheckOut\ProjectRoot\AOTO_ZZ_qiyeshengmingzhouqi\AOTO_qiyeshengmingzhouqi_11.3(5760x2160)\AOTO_SMZQ_Data\StreamingAssets\科创金融\创新积分贷.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\AOTO_ZZ_qiyeshengmingzhouqi\AOTO_qiyeshengmingzhouqi_11.3(5760x2160)\AOTO_SMZQ_Data\StreamingAssets\科创金融\改革试验区建设赋能.png",
           @"D:\SVN_CheckOut\ProjectRoot\AOTO_ZZ_qiyeshengmingzhouqi\AOTO_qiyeshengmingzhouqi_11.3(5760x2160)\AOTO_SMZQ_Data\StreamingAssets\科创金融\金雨育苗行动.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\AOTO_ZZ_qiyeshengmingzhouqi\AOTO_qiyeshengmingzhouqi_11.3(5760x2160)\AOTO_SMZQ_Data\StreamingAssets\科创金融\科技金融内涵.png",
          @"D:\SVN_CheckOut\ProjectRoot\AOTO_ZZ_qiyeshengmingzhouqi\AOTO_qiyeshengmingzhouqi_11.3(5760x2160)\AOTO_SMZQ_Data\StreamingAssets\科创金融\全生命周期服务方案.png",
        @"D:\SVN_CheckOut\ProjectRoot\AOTO_ZZ_qiyeshengmingzhouqi\AOTO_qiyeshengmingzhouqi_11.3(5760x2160)\AOTO_SMZQ_Data\StreamingAssets\科创金融\人行科创再贷款.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\AOTO_ZZ_qiyeshengmingzhouqi\AOTO_qiyeshengmingzhouqi_11.3(5760x2160)\AOTO_SMZQ_Data\StreamingAssets\科创金融\中国银行山东省分行：服务构建新发展格局 为高质量发展注入金融力量.jpg",
          @"D:\SVN_CheckOut\ProjectRoot\AOTO_ZZ_qiyeshengmingzhouqi\AOTO_qiyeshengmingzhouqi_11.3(5760x2160)\AOTO_SMZQ_Data\StreamingAssets\科创金融\中银厂房贷.jpg",
           @"D:\SVN_CheckOut\ProjectRoot\AOTO_ZZ_qiyeshengmingzhouqi\AOTO_qiyeshengmingzhouqi_11.3(5760x2160)\AOTO_SMZQ_Data\StreamingAssets\科创金融\full\1科技金融内涵.png",
            @"D:\SVN_CheckOut\ProjectRoot\AOTO_ZZ_qiyeshengmingzhouqi\AOTO_qiyeshengmingzhouqi_11.3(5760x2160)\AOTO_SMZQ_Data\StreamingAssets\科创金融\full\1中银集团助力科技金融.png"
        };
        //不关心  只提前加载  后期get(提示)
        //加载，并需要加载完成执行
        //加载，并控制加载细节
        ImageLoadManager.Instance.LoadImage("");
        ImageLoadManager.Instance.LoadImage(new string[] { "","",null});
        ImageLoadManager.Instance.LoadImage(tempstr2);
        ImageLoadManager.Instance.LoadImage(@"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg",(sp)=> {

            Debug.Log("?");
            rawImage.texture = sp[0];
        });

        ImageLoadManager.Instance.LoadImage(tempstr);
        ImageLoadManager.Instance.LoadImage(tempstr3);
        ImageLoadManager.Instance.LoadImage(tempstr1);

        ImageLoadManager.Instance.LoadImage(tempstr2);

  
        GameObject tempobj =  ImageLoadManager.Instance.gameObject;

       // rawImage.texture  = ImageLoadManager.Instance.GetTexture2D(@"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg");
        //ImageLoadManager.Instance.LoadImage("");
        //StartCoroutine( ImageLoadManager.Instance.WWWLoadImage_Test(@"C:\Users\DS\Desktop\郊庙歌辞 晋昭德成\郊庙歌辞 晋昭德成.jpg"));
        //StartCoroutine(ImageLoadManager.Instance.WWWLoadImage_Test(@"C:\Users\DS\Desktop\郊庙歌辞 晋昭德成\郊庙歌辞 晋昭德成 - 副本.bmp"));
        //StartCoroutine(ImageLoadManager.Instance.WWWLoadImage_Test(@"D:\BaiduNetdiskDownload\汽车分期图片old.png"));
        //   StartCoroutine(ImageLoadManager.Instance.UnityWWWLoadImage_GetByte(@"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg"));
        Debug.Log(tempobj.name);
        //rawImage.texture = ImageLoadManager.Instance.tempTex;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ImageLoadManager.Instance.LoadImage(@"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg");
           // rawImage.texture = ImageLoadManager.Instance.tempTex;
        }

        if (Input.GetKey(KeyCode.B))
        {
            ImageLoadManager.Instance.LoadImage(@"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg");
            // rawImage.texture = ImageLoadManager.Instance.tempTex;
        }

        if (Input.GetKey(KeyCode.C))
        {
            ImageLoadManager.Instance.LoadImage(@"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg");
            // rawImage.texture = ImageLoadManager.Instance.tempTex;
        }

        if (Input.GetKey(KeyCode.D))
        {
            ImageLoadManager.Instance.LoadImage(@"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg");
            // rawImage.texture = ImageLoadManager.Instance.tempTex;
        }

        if (Input.GetKey(KeyCode.T))
        {
            //StartCoroutine(ImageLoadManager.WWWLoadImage_Texture2D());
           //ImageLoadManager.Instance.LoadImage(@"D:\BaiduNetdiskDownload\企业手机银行 - 副本.jpg");
            // rawImage.texture = ImageLoadManager.Instance.tempTex;
        }

    }
}
