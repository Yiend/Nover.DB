using Nover.DB.Core;
using System;
using System.Collections.Generic;

namespace Nover.DB.Data
{
    internal class XmlCommandCustomizeLoader : BaseResourceConfigLoader
    {

        private readonly List<ConfigFileLoadArgs> _items = new List<ConfigFileLoadArgs>();

        /// <summary>
        /// 是否能处理指定的配置文件
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool CanLoad(ConfigFileLoadArgs args)
        {
            return args.Assembly == null && args.FileName.EndsWith(".XmlCommand.config", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 读取某个配置文件
        /// </summary>
        /// <param name="args"></param>
        public override void LoadFile(ConfigFileLoadArgs args)
        {
            this._items.Add(args);
        }

        /// <summary>
        /// 结束所有的配置读取操作，用于更新缓存变量。
        /// </summary>
        public override void EndLoad()
        {
            foreach (ConfigFileLoadArgs item in _items)
            {
                XmlCommandManager.LoadCustomizeXml(item.DbType, item.FileContent);
            }
        }
    }
}