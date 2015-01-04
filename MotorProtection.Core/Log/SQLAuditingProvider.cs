using MotorProtection.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MotorProtection.Core.Log
{
    public class SQLAuditingProvider : LogProvider
    {
        public override DateTime Add(string level, int? userId, Dictionary<string, string> attributes)
        {
            DateTime now = DateTime.Now;

            XmlDocument xmlAttr = new XmlDocument();

            XmlElement xmlItems = xmlAttr.CreateElement("Items");
            xmlAttr.AppendChild(xmlItems);

            if (attributes != null)
            {
                foreach (var entry in attributes)
                {
                    XmlElement xmlItem = xmlAttr.CreateElement("Item");

                    XmlAttribute xmlItemName = xmlAttr.CreateAttribute("Name");
                    xmlItemName.Value = entry.Key;
                    xmlItem.Attributes.Append(xmlItemName);

                    xmlItem.InnerText = entry.Value == null ? string.Empty : entry.Value;

                    xmlItems.AppendChild(xmlItem);
                }
            }

            Auditing log = new Auditing
            {
                Level = Convert.ToInt32(level),
                UserID = userId,
                Attributes = xmlAttr.InnerXml,
                CreateTime = now
            };

            try
            {
                using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                {
                    ctt.Auditings.AddObject(log);
                    ctt.SaveChanges();
                }
            }
            catch { }

            return now;
        }
    }
}
