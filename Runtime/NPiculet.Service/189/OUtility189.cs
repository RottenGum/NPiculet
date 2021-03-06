﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web;
using System.IO;

namespace NPiculet.Service.E189
{
    public class Utility189
    {
        private string _plaintext;

        /// <summary>
        /// return a yyyy-MM-dd hh:mm:ss formate time
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDate()
        {
            DateTime today = DateTime.Now;
            return today.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// return a yyyyMMddhhmmss formate time
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDate2()
        {
            DateTime today = DateTime.Now;
            string strDate = today.ToString("yyyyMMddHHmmss");
            return strDate;
        }

        /// <summary>
        /// Build a plaintext for encryption,
        /// format is like key1=value1&key2=value2&key3=value3
        /// </summary>
        /// <param name="paramlist">A dictionary contains all parameters</param>
        /// <returns>plaintext</returns>
        private static string buildPlainText(IDictionary<string,string> paramlist)
        {
            string plaintext = string.Empty;
            int count = 0;
            foreach (KeyValuePair<string, string> pair in paramlist)
            {
                plaintext += pair.Key + "=" + pair.Value;
                if (count < paramlist.Count-1)
                    plaintext += "&";
                count++;
            }
            return plaintext;
        }

        /// <summary>
        /// MD5 encryption by plaintext
        /// </summary>
        /// <param name="plaintext">plaintext</param>
        /// <returns>ciphertext</returns>
        public static string MD5(string plaintext)
        {
            byte[] result = Encoding.Default.GetBytes(plaintext);
            return MD5(result);
        }

        /// <summary>
        /// MD5 encryption by parameters
        /// </summary>
        /// <param name="signParams">encrpt parameters</param>
        /// <param name="appSecret">app_secret</param>
        /// <returns></returns>
        public static string MD5(IDictionary<string,string> signParams,String appSecret) 
        {
            string plaintext = appSecret + "&" + buildPlainText(signParams);
            return MD5(plaintext);
        }

        /// <summary>
        /// MD5 encryption by binary
        /// </summary>
        /// <param name="binaryData">a byte array for plaintext</param>
        /// <returns></returns>
        public static string MD5(byte[] binaryData)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(binaryData);
            return BitConverter.ToString(output).Replace("-", "");  
        }


        /// <summary>
        /// MHAC_SHA1 Encryption
        /// </summary>
        /// <param name="plaintext"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected static byte[] HMAC_SHA1(string plaintext, string key)
        {
            UTF8Encoding utf8encoding = new UTF8Encoding();
            byte[] keybyte =  utf8encoding.GetBytes(key);
            byte[] contentbyte = utf8encoding.GetBytes(plaintext);
            byte[] cipherbyte;
            using(HMACSHA1 hmacsha1 = new HMACSHA1(keybyte))
            {
               cipherbyte = hmacsha1.ComputeHash(contentbyte);
            }

           // return utf8encoding.GetString(cipherbyte);
            return cipherbyte;
        }

        /// <summary>
        /// Base64 Encryption
        /// </summary>
        /// <param name="cipherbyte"></param>
        /// <returns></returns>
        protected static string Base64Encrypt(byte[] cipherbyte) 
        {
            string base64str = Convert.ToBase64String(cipherbyte);
            return base64str;
        }

        /// <summary>
        /// HMAC_SHA1 Encryption
        /// </summary>
        /// <param name="plaintext">plaintext</param>
        /// <param name="key">key</param>
        /// <returns>urlencoded ciphertext</returns>
        public static string DoSignature(string plaintext,string key)
        {
            return  HttpUtility.UrlEncode(Base64Encrypt(HMAC_SHA1(plaintext, key)));
        }

        /// <summary>
        /// HMAC_SHA1 Encryption
        /// </summary>
        /// <param name="parameters">parameters for generate plaintext</param>
        /// <param name="key">key</param>
        /// <returns>urlencoded ciphertext</returns>
        public static string DoSignature(SortedDictionary<string, string> parameters, string key)
        {
            string plaintext = buildPlainText(parameters);
            return HttpUtility.UrlEncode(Base64Encrypt(HMAC_SHA1(plaintext, key)));
        }


        /// <summary>
        /// open a FileStream to read file content into a byte array.
        /// </summary>
        /// <returns>byte array contains binary data of the input file</returns>
        public static byte[] ReadFile(string filepath)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                byte[] byteData = new byte[fs.Length];
                fs.Read(byteData, 0, byteData.Length);
                fs.Close();
                return byteData;
            }
        }

		/// <summary>
		/// 将字典转换为名称、值的集合
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static NameValueCollection GetCollection(IDictionary<string, string> args)
		{
			NameValueCollection collection = new NameValueCollection();
			if (args != null) {
				foreach (KeyValuePair<string, string> pair in args) {
					collection.Add(pair.Key, pair.Value);
				}
			}
			return collection;
		}

		/// <summary>
		/// 将字典转换为名称、值的集合
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string GetUrlParameters(IDictionary<string, string> args)
		{
			string parms = "";
			if (args != null) {
				foreach (KeyValuePair<string, string> pair in args) {
					if (!string.IsNullOrEmpty(parms)) parms += "&";
					parms += pair.Key + "=" + pair.Value;
				}
			}
			return parms;
		}

		/// <summary>
		/// 将字典转化为字节流
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
	    public static byte[] GetDataParm(IDictionary<string, string> args)
	    {
		    string parm = GetUrlParameters(args);
		    return Encoding.UTF8.GetBytes(parm);
	    }

		/// <summary>
		/// 向指定地址发送 POST 请求
		/// </summary>
		/// <param name="url"></param>
		/// <param name="data"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static string PostWebRequest(string url, string data, Encoding encoding)
		{
			string ret = string.Empty;

			byte[] byteArray = encoding.GetBytes(data); //转化
			HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
			webReq.Method = "POST";
			webReq.ContentType = "application/x-www-form-urlencoded";

			webReq.ContentLength = byteArray.Length;
			Stream newStream = webReq.GetRequestStream();
			newStream.Write(byteArray, 0, byteArray.Length);//写入参数
			newStream.Close();
			HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
			StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
			ret = sr.ReadToEnd();
			sr.Close();
			response.Close();
			newStream.Close();

			return ret;
		}

    }
}
