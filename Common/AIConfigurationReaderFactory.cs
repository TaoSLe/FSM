using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
	/// <summary>
	///  AI配置文件读取器工厂：负责缓存配置文件读取器。
	/// </summary>
	public class AIConfigurationReaderFactory 
	{
        private static Dictionary<string, AIConfigurationReader> cache;
        static AIConfigurationReaderFactory()
        {
            cache = new Dictionary<string, AIConfigurationReader>();
        }

        public static Dictionary<string, Dictionary<string, string>> GetConfigMap(string fileName)
        {
            //如果指定配置文件不存在读取器对象，则创建。
            if (!cache.ContainsKey(fileName))
            {
                var reader = new AIConfigurationReader(fileName);
                cache.Add(fileName, reader);
            }
            return cache[fileName].map;
        }
	}
}
