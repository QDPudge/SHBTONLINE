using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web.Services.Description;
using Microsoft.CSharp;

namespace NanJingMonitorDB
{
    public static class WebServiceExtension
    {
        private static readonly Dictionary<string, CompilerResults> htWebService = new Dictionary<string, CompilerResults>();
        
        /// <summary>
        /// 动态调用WebService
        /// </summary>
        /// <param name="request">WebService请求参数</param>
        /// <returns>object</returns>
        public static object InvokeWebService(this WebServiceRequest request)
        {
            var cr = request.GetCompilerResults();
            if (cr == null) return null;

            //生成代理实例,并调用方法
            var assembly = cr.CompiledAssembly;
            var t = assembly.GetType(request.NameSpace + "." + request.ClassName, true, true);
            var obj = Activator.CreateInstance(t);
            var mi = t.GetMethod(request.MethodName);
            return mi.Invoke(obj, request.Args);
        }

        private static CompilerResults GetCompilerResults(this WebServiceRequest request)
        {

            if (request == null) return null;
            if (string.IsNullOrEmpty(request.ClassName)) request.ClassName = GetClassName(request.Url);
            var strUrl = string.Format("{0}?wsdl", request.Url);
            if (htWebService.ContainsKey(strUrl)) return htWebService[strUrl];


            //获取服务描述语言(WSDL)
            var wc = new WebClient();

            var stream = wc.OpenRead(strUrl);
            if (stream == null) return null;

            var sd = ServiceDescription.Read(stream);
            var sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, "", "");
            var cn = new CodeNamespace(request.NameSpace);

            //生成客户端代理类代码
            var ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            var csc = new CSharpCodeProvider();
            var icc = csc.CreateCompiler();

            //设定编译器的参数
            var cplist = new CompilerParameters { GenerateExecutable = false, GenerateInMemory = true };
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");

            //编译代理类
            var cr = icc.CompileAssemblyFromDom(cplist, ccu);
            if (cr.Errors.HasErrors)
            {
                var sb = new StringBuilder();
                foreach (CompilerError ce in cr.Errors)
                {
                    sb.Append(ce);
                    sb.Append(Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }

            htWebService.Add(strUrl, cr);
            return cr;
        }

        /// <summary>
        /// 获取WebService名称
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetClassName(string url)
        {
            var parts = url.Split('/');
            var pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }
    }

    /// <summary>
    /// WebService请求参数封装
    /// </summary>
    public class WebServiceRequest
    {
        /// <summary>
        /// WebService路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// WebService文件命名空间
        /// </summary>
        public string NameSpace { get; set; }
        /// <summary>
        /// 请求方法名
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public object[] Args { get; set; }
        /// <summary>
        /// WebService类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }


    }
}
