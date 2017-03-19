using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.IO;

namespace SqlConnect
{
    public class DaoConfigProvider
    {
        public static DaoConfig Build(Assembly sqlSourceAssembly, string configFile, string connectionKey)
        {
            DaoConfig config = new DaoConfig();

            config.ConnectionString= System.Configuration.ConfigurationManager.AppSettings[connectionKey];
            XDocument xdoc = GetXDocument(configFile, sqlSourceAssembly);
            ProcessSqlResources(config, xdoc, sqlSourceAssembly);

            return config;
        }

        private static XDocument GetXDocument(string file,Assembly sourceAssembly)
        {
            XDocument xdoc = null;

            if (sourceAssembly == null)
            {
                xdoc = XDocument.Load(file);
            }
            else
            {
                Stream stream = GetStreamFromAssembly(sourceAssembly, file);
                StreamReader reader = new StreamReader(stream);
                string content = reader.ReadToEnd();
                xdoc = XDocument.Parse(content);
            }
            return xdoc;
        }
     
        private static void ProcessSqlResources(DaoConfig config, XDocument xdoc,Assembly assembey)
        {
            IEnumerable<XElement> resources = xdoc.Root.Element("SqlResources").Elements("SqlResource");
            foreach (var item in resources)
            {
                string url = item.Attribute("Url").Value;
                SqlResource resource = GetSqlResource(url, assembey);
                config.AddResource(resource);
            }
        }

        private static SqlResource GetSqlResource(string url,Assembly assembly)
        {
            SqlResource resource = new SqlResource();
            resource.Url = url;

            XDocument resourceDoc = GetXDocument(url, assembly);

            var xsqls=resourceDoc.Root.Elements("sql");
            foreach (var xsql in xsqls)
            {
                Sql sql = new Sql();
                sql.Id = xsql.Attribute("id").Value;
                sql.Text = xsql.ToString().Replace("\n", " ");
                resource.AddSql(sql);
            }

            return resource;
        }


        private static string GetResourceName(Assembly assembly, string file)
        {
            file = file.Replace("/", ".");
            string sourceName = string.Format("{0}.{1}", assembly.GetName().Name, file);
            return sourceName;
        }

        private static Stream GetStreamFromAssembly(Assembly assembly, string file)
        {
            string sourceName = GetResourceName(assembly, file);
            Stream stream = assembly.GetManifestResourceStream(sourceName);
            if (stream == null)
            {
                string err = string.Format("Can not get Resource(name={0}) from assembly(name={1}),出现这种情况的可能是没有把配置文件的属性设置为嵌入的资源", file, assembly.FullName);
                throw new Exception(err);
            }

            return stream;
        }
    }


}
