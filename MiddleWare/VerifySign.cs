using Microsoft.AspNetCore.Http;
using MiddleWarePrac.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWarePrac.MiddleWare
{
    public class VerifySign
    {
        private RequestDelegate _next;

        //=> {get;} 同意
        private string MD5Key => "jhkhjkqwkafjkcvkasdkfpewrerewr";
        public VerifySign(RequestDelegate next)
        {
            _next = next;
        }

        //https://www.cnblogs.com/xmai/p/10458454.html
        //middleware 坑
        //在向客户端发送响应(response)后，不允许调用 next.Invoke，会引发异常 (但是範例有這樣用..覺得怪)

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == "GET")
            {
                string username= context.Request.Query["username"].ToString();
                string token = context.Request.Query["token"].ToString();

                //驗簽失敗返回
                if (!Verify(username, token))
                {
                    //這邊沒有寫_next 就不會傳到下一層
                    await context.Response.WriteAsync("token 錯誤 \r\n");
                    

                }
                

            }


            if(context.Request.Method == "POST")
            {
                string reqbody;
                //讀body 數據
                using (var reader = new StreamReader(context.Request.Body))
                {
                    reqbody = await reader.ReadToEndAsync();
                    //數據流指針回到起點
                    context.Request.Body.Seek(0, SeekOrigin.Begin);
                }

                string path = context.Request.Path.ToString();
                //對應到相對的model
                switch (path)
                {
                    case "/login":
                        break;
                    case "/register":
                        break;
                    case "/directory":
                        break;
                    case "/memberinfo":
                        break;
                }

                
                

                string username = context.Request.Query["username"].ToString();
                string token = context.Request.Query["token"].ToString();
            }



            await _next.Invoke(context);

        }


        //簽名方法
        public bool Verify(string username,string token)
        {
            string signsource = $"username={username}&key={MD5Key}";
            string sign = MD5util.Hash(signsource);
            //true or false
            return string.Equals(sign,token);
        }

    }
}
