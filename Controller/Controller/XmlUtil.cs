﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;

namespace Controller
{    
    class XmlUtil
    {
        string xmlpath = "modules.xml";
        /// <summary>
        /// 获取xml内容 转换为table
        /// </summary>
        /// <returns></returns>
        public DataTable GetXmlTable()
        {
            DataTable dt = new DataTable();
            DataSet xmlds = new DataSet();
            xmlds.ReadXml(xmlpath);
            if (xmlds != null && xmlds.Tables.Count > 0)
            {
                dt = xmlds.Tables[0];
            }

            return dt;
        }

        /// <summary>
        /// 新建module
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="switches"></param>
        public void AddModule(string name, string ip, string port, string[] switches)
        {
            XmlElement module = null, root = null;
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(xmlpath);
            root = xmldoc.DocumentElement;

            //---  添加module ----
            module = xmldoc.CreateElement("module");
            //名称
            XmlElement eleName = xmldoc.CreateElement("name");
            eleName.InnerText = name;
            module.AppendChild(eleName);
            //ip
            XmlElement eleIP = xmldoc.CreateElement("ip");
            eleIP.InnerText = ip;
            module.AppendChild(eleIP);
            //端口
            XmlElement elePort = xmldoc.CreateElement("port");
            elePort.InnerText = port;
            module.AppendChild(elePort);

            for (int i = 0; i < switches.Length; i++)
            {
                XmlElement ele = xmldoc.CreateElement("switch" + (i + 1));
                ele.InnerText = switches[i];
                module.AppendChild(ele);
            }

            root.AppendChild(module);

            xmldoc.Save(xmlpath);
        }

        public void DelModule(string ip)
        {
            XmlElement module = null, root = null;
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(xmlpath);
            root = xmldoc.DocumentElement;
            module = (XmlElement)root.SelectSingleNode("/root/module[ip='" + ip + "']");
            root.RemoveChild(module);
            xmldoc.Save(xmlpath);
        }

        public void UpdateModule(string name, string oldIP, string ip, string port, string[] switches)
        {
            XmlElement module = null, root = null;
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(xmlpath);
            root = xmldoc.DocumentElement;

            module = (XmlElement)root.SelectSingleNode("/root/module[ip='" + oldIP + "']");
            module.GetElementsByTagName("name").Item(0).InnerText = name;
            module.GetElementsByTagName("ip").Item(0).InnerText = ip;
            module.GetElementsByTagName("port").Item(0).InnerText = port;
            for (int i = 0; i < switches.Length; i++)
            {
                module.GetElementsByTagName("switch" + (i + 1)).Item(0).InnerText = switches[i];
            }
            xmldoc.Save(xmlpath);
        }
    }
}