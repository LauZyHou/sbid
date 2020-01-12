using System;
using System.Collections.Generic;
using System.Text;

namespace sbid.Model
{
    // 管理资源
    public class ResourceManager
    {
        public static Protocol currentProtocal = null;// 在内存中记录当前用户所在的Protocal
        // 管理所有的Protocal
        public static List<Protocol> protocals = new List<Protocol>();

    }
}
