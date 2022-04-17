using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebApplication3.Areas.Login.Controllers
{
    public class VerifyCodeHelper
    {
        #region 验证码长度
        int _length = 4;
        /// <summary>
        /// 验证码长度
        /// </summary>
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }
        #endregion

        #region 验证码字体大小
        int _fontSize = 30;
        /// <summary>
        /// 验证码字体大小
        /// </summary>
        public int FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }
        #endregion

        #region 边框补(默认1像素)
        int _padding = 1;
        /// <summary>
        /// 边框补(默认1像素)
        /// </summary>
        public int Padding
        {
            get { return _padding; }
            set { _padding = value; }
        }
        #endregion

        #region 是否输出燥点
        bool _chaos = true;
        /// <summary>
        /// 是否输出燥点
        /// </summary>
        public bool Chaos
        {
            get { return _chaos; }
            set { _chaos = value; }
        }
        #endregion

        #region 自定义背景色(默认白色)
        Color _backgroundColor = Color.White;
        /// <summary>
        /// 自定义背景色(默认白色)
        /// </summary>
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }
        #endregion

        #region 自定义随机颜色数组
        Color[] _colors = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        /// <summary>
        /// 自定义随机颜色数组
        /// </summary>
        public Color[] Colors
        {
            get { return _colors; }
            set { _colors = value; }
        }
        #endregion

        #region 自定义字体数组
        string[] _fonts = { "Arial", "Georgia", "Segoe Script" };
        /// <summary>
        /// 自定义字体数组
        /// </summary>
        public string[] Fonts
        {
            get { return _fonts; }
            set { _fonts = value; }
        }
        #endregion

        #region 自定义随机码字符串序列(使用逗号分隔)
        //string codeSerial = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        string _codeSerial = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";
        /// <summary>
        /// 自定义随机码字符串序列(使用逗号分隔)
        /// </summary>
        public string CodeSerial
        {
            get { return _codeSerial; }
            set { _codeSerial = value; }
        }
        #endregion

        #region 生成校验码图片
        /// <summary>
        /// 生成校验码图片
        /// </summary>
        /// <param name="code">校验码</param>
        /// <returns></returns>
        public Bitmap CreateImageCode(string code)
        {
            int fSize = FontSize;
            int fWidth = fSize + Padding;

            int imageWidth = (int)(code.Length * fWidth) + 4 + Padding * 2;
            int imageHeight = fSize * 2 + Padding;
            Bitmap image = new Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(image);
            g.Clear(BackgroundColor);
            Random rand = new Random();
            //后景线
            if (this.Chaos)
            {
                Pen pen;
                int c = Length * 10;
                for (int i = 0; i < c; i++)
                {
                    int cchaosindex = rand.Next(Colors.Length - 1);
                    pen = new Pen(Colors[cchaosindex], 0);
                    int x = rand.Next(image.Width);
                    int x1 = rand.Next(image.Width);
                    int y = rand.Next(image.Height);
                    int y1 = rand.Next(image.Height);
                    g.DrawLine(pen, x, y, x1, y1);

                }
            }

            #region 验证码

            int left = 0, top = 0, top1 = 1, top2 = 1;

            int n1 = (imageHeight - FontSize - Padding * 2);
            int n2 = n1 / 4;
            top1 = n2;
            top2 = n2 * 2;

            Font f;
            Brush b;

            int cindex, findex;

            //随机字体和颜色的验证码字符
            for (int i = 0; i < code.Length; i++)
            {
                cindex = rand.Next(Colors.Length - 1);
                findex = rand.Next(Fonts.Length - 1);

                f = new Font(Fonts[findex], fSize, System.Drawing.FontStyle.Bold);
                b = new SolidBrush(Colors[cindex]);

                if (i % 2 == 1)
                {
                    top = top2;
                }
                else
                {
                    top = top1;
                }

                left = i * fWidth;

                g.DrawString(code.Substring(i, 1), f, b, left, top);
            }

            #endregion

            //给背景添加随机生成的燥点
            if (this.Chaos)
            {
                Pen pen;
                int c = Length * 20;
                for (int i = 0; i < c; i++)
                {
                    int cchaosindex = rand.Next(Colors.Length - 1);
                    pen = new Pen(Colors[cchaosindex], 0);
                    int x = rand.Next(image.Width);
                    int y = rand.Next(image.Height);
                    g.DrawRectangle(pen, x, y, 1, 1);
                }
            }
            //画一个边框 边框颜色为Color.Gainsboro
            g.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, image.Width - 1, image.Height - 1);
            g.Dispose();
            return image;
        }
        #endregion

        #region 将创建好的图片输出到页面
        /// <summary>
        /// 将创建好的图片输出到页面
        /// </summary>
        /// <param name="code">校验码</param>
        /// <param name="context">页面句柄</param>
        public void CreateImageOnPage(string code, HttpContext context)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            Bitmap image = this.CreateImageCode(code);
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            context.Response.ClearContent();
            context.Response.ContentType = "image/Jpeg";
            context.Response.BinaryWrite(ms.GetBuffer());
            ms.Close();
            ms = null;
            image.Dispose();
            image = null;
        }
        #endregion

        #region 生成随机字符码
        /// <summary>
        /// 生成随机字符码
        /// </summary>
        /// <param name="codeLen">生成的长度</param>
        /// <returns></returns>
        public string CreateVerifyCode(int codeLen = 0)
        {
            if (codeLen == 0) { codeLen = Length; }
            string[] arr = CodeSerial.Split(',');
            string code = ""; int randValue = -1;
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);
                code += arr[randValue];
            }
            return code;
        }
        #endregion

    }
}