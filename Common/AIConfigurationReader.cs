using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AI.FSM
{
	/// <summary>
	///  
	/// </summary>
	public class AIConfigurationReader 
	{
        //缺点：
        //  如果静态成员，只能有一份数据。
        //public static Dictionary<string, Dictionary<string, string>> map;

        //static AIConfigurationReader()
        //{
        //    map = new Dictionary<string, Dictionary<string, string>>();
        //    string configFile = ConfigurationReader.GetConfigFile("AI_01.txt");
        //    ConfigurationReader.ReaderFile(configFile, BuildMap);
        //}

        //private static string mainKey;
        //private static void BuildMap(string line)
        //{
        //    if (line == "") return;
        //    if (line[0] == '[')
        //    {
        //        //状态[Idle]
        //        mainKey =  line.Substring(1, line.Length - 2);
        //        map.Add(mainKey, new Dictionary<string, string>());             
        //    }
        //    else
        //    {
        //        //映射 NoHealth>Dead
        //        string[] keyValue = line.Split('>');
        //        map[mainKey].Add(keyValue[0], keyValue[1]);
        //    }
        //}

        public Dictionary<string, Dictionary<string, string>> map;

        public AIConfigurationReader(string fileName)
        {
            map = new Dictionary<string, Dictionary<string, string>>();
            string configFile = ConfigurationReader.GetConfigFile(fileName);
            ConfigurationReader.ReaderFile(configFile, BuildMap);
        }

        private string mainKey;
        private void BuildMap(string line)
        {
            if (line == "") return;
            if (line[0] == '[')
            {
                //状态[Idle]
                mainKey = line.Substring(1, line.Length - 2);
                map.Add(mainKey, new Dictionary<string, string>());
            }
            else
            {
                //映射 NoHealth>Dead
                string[] keyValue = line.Split('>');
                map[mainKey].Add(keyValue[0], keyValue[1]);
            }
        }
    }
}
