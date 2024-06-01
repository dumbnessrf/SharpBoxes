using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SharpBoxes.Helpers;

public static class SystemHelpers
{
    public class SystemHelper
    {
        private static string dateFormat = "yyyy-MM-dd";

        private static bool ObjectIsNullOrEmpty(object objData)
        {
            bool isNull = false;

            if (objData == null || objData == DBNull.Value || objData.ToString() == string.Empty)
            {
                isNull = true;
            }

            return isNull;
        }

        private static DateTime GetDateTimeByString(string sDate, string sFormat)
        {
            DateTime dtm = DateTime.ParseExact(
                sDate,
                sFormat,
                System.Globalization.CultureInfo.CurrentCulture
            );
            return dtm;
        }

        private static GatherHardware1Model CheckIsGather(string section, string key)
        {
            #region 用于参考的结点值
            //电脑型号：
            //Win32_BaseBoard
            //Product:    N551JM
            //Manufacturer:    ASUSTeK COMPUTER INC.


            //电脑名称：
            //Win32_Processor
            //SystemName:    DESKTOP-77MM5C2


            //操作系统：
            //Win32_OperatingSystem
            //Caption:    Microsoft Windows 10 专业版
            //OSArchitecture:    64 位


            //CPU：
            //Win32_Processor
            //Name:    Intel(R) Core(TM) i7-4710HQ CPU @ 2.50GHz

            //内存：
            //Win32_OperatingSystem
            //TotalVisibleMemorySize:    16657336
            //Win32_PhysicalMemory
            //Speed：1600


            //主板：
            //Win32_BIOS
            //Caption:    N551JM.202
            //ReleaseDate:    20140717000000.000000+000


            //主硬盘：
            //Win32_DiskDrive
            //Caption:    Samsung SSD 870 EVO 1TB
            //Caption:    Samsung SSD 840 EVO 500GB


            //显卡：
            //Win32_VideoController
            //Caption:    NVIDIA GeForce GTX 860M

            //显示器
            //Win32_DesktopMonitor
            //PNPDeviceID:    DISPLAY\AUO36ED\4&37E9C966&0&UID265988


            //声卡：
            //Win32_SoundDevice
            //Caption:    Realtek High Definition Audio


            //网卡：
            //Win32_NetworkAdapter
            //Manufacturer:    Microsoft
            //Manufacturer:    Realtek
            //Name:    Realtek PCIe GbE Family Controller


            //Win32_NetworkAdapterConfiguration
            //MACAddress:    54:27:1E:D9:4C:F6
            //IPAddress  192.168.1.231;   fe80::e369:a4:1748:a692;


            //登录用户
            //Win32_ComputerSystem
            //UserName:    DESKTOP-77MM5C2\Administrator
            #endregion

            List<GatherHardware1Model> listGather = new List<GatherHardware1Model>();
            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "PcBrand",
                    displayText = "电脑型号",
                    sectionName = "Win32_BaseBoard",
                    keyName = "Manufacturer",
                    sortNo = 1
                }
            );
            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "PcBrand",
                    displayText = "电脑型号",
                    sectionName = "Win32_BaseBoard",
                    keyName = "Product",
                    sortNo = 2,
                }
            );

            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "PcName",
                    displayText = "电脑名称",
                    sectionName = "Win32_Processor",
                    keyName = "SystemName",
                    sortNo = 3,
                }
            );

            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "LoginUser",
                    displayText = "登录用户",
                    sectionName = "Win32_ComputerSystem",
                    keyName = "UserName",
                    sortNo = 4,
                }
            );

            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "OsName",
                    displayText = "操作系统",
                    sectionName = "Win32_OperatingSystem",
                    keyName = "Caption",
                    sortNo = 5,
                }
            );
            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "OsName",
                    displayText = "操作系统",
                    sectionName = "Win32_OperatingSystem",
                    keyName = "OSArchitecture",
                    sortNo = 6,
                }
            );

            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "CpuInfo",
                    displayText = "CPU",
                    sectionName = "Win32_Processor",
                    keyName = "Name",
                    sortNo = 7,
                }
            );

            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "MemoryInfo",
                    displayText = "内存",
                    sectionName = "Win32_OperatingSystem",
                    keyName = "TotalVisibleMemorySize",
                    sortNo = 8,
                }
            );

            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "BiosInfo",
                    displayText = "主板",
                    sectionName = "Win32_BIOS",
                    keyName = "Caption",
                    sortNo = 10,
                }
            );
            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "BiosInfo",
                    displayText = "主板",
                    sectionName = "Win32_BIOS",
                    keyName = "ReleaseDate",
                    sortNo = 11,
                }
            );

            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "DriveInfo",
                    displayText = "主硬盘",
                    sectionName = "Win32_DiskDrive",
                    keyName = "Caption",
                    sortNo = 12,
                }
            );

            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "VideoCard",
                    displayText = "显卡",
                    sectionName = "Win32_VideoController",
                    keyName = "Caption",
                    sortNo = 13,
                }
            );

            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "Displayer",
                    displayText = "显示器",
                    sectionName = "Win32_DesktopMonitor",
                    keyName = "PNPDeviceID",
                    sortNo = 14,
                }
            );

            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "AudioCard",
                    displayText = "声卡",
                    sectionName = "Win32_SoundDevice",
                    keyName = "Caption",
                    sortNo = 15,
                }
            );

            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "IpAddress",
                    displayText = "IP地址",
                    sectionName = "Win32_NetworkAdapterConfiguration",
                    keyName = "IPAddress",
                    sortNo = 18,
                }
            );

            listGather.Add(
                new GatherHardware1Model()
                {
                    dbColumnName = "ErpUser",
                    displayText = "ERP用户",
                    sectionName = "",
                    keyName = "",
                    sortNo = 19,
                }
            );

            var modelP = listGather.FirstOrDefault(
                c => c.sectionName == section && c.keyName == key
            );
            return modelP;
        }

        /// <summary>
        /// <example>
        /// <strong>结果类似如下</strong>
        /// <code>
        /// 电脑型号：LENOVO-LNVNB****216
        /// 电脑名称：LAPTOP-BQ***7GH
        /// 登录用户：LAPTOP-BQ****GH\zheng
        /// 操作系统：Microsoft Windows 11 家庭中文版-64 位
        /// CPU：AMD Ryzen 7 5700U with Radeon Graphics
        /// 内存：13.83G
        /// 主板：GQCN26WW(V1.12)-2021-08-23
        /// 主硬盘：WDC PC SN530 SDBPMPZ-512G-1101-SanDisk Extreme 55AE SCSI Disk Device-TOSHIBA EXTERNAL_USB USB Device
        /// 显卡：OrayIddDriver Device-AMD Radeon(TM) Graphics
        /// 显示器：-DISPLAY\AUO683D\5655AEAD1UID256
        /// 声卡：Realtek High Definition Audio-AMD Streaming Audio Device-AMD High Definition Audio Device
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static string GetHardwareInfo()
        {
            var a = new StringBuilder();
            string str = "";
            GetHardwareInfo(
                ref a,
                s =>
                {
                    str += s + Environment.NewLine;
                }
            );
            return str;
        }

        public static void GetHardwareInfo(out List<string> infos)
        {
            infos = GetHardwareInfo()
                .Split([$"{Environment.NewLine}"], StringSplitOptions.None)
                .ToList();
        }

        public static void GetHardwareInfo(out Dictionary<string, string> infos)
        {
            infos = new Dictionary<string, string>();
            var a = GetHardwareInfo()
                .Split([$"{Environment.NewLine}"], StringSplitOptions.None)
                .ToList();
            foreach (var element in a)
            {
                var data = element.Split('：');
                if (data.Length != 2)
                {
                    continue;
                }
                infos.Add(data[0], data[1]);
            }
        }

        private static string GetHardwareInfo(
            ref StringBuilder StrBuilder,
            Action<string> del_write = null
        )
        {
            List<GatherHardware1Model> listGather = new List<GatherHardware1Model>();

            HardwareEnum[] hardWareEnums = Enum.GetValues(typeof(HardwareEnum)) as HardwareEnum[];
            foreach (var item in hardWareEnums)
            {
                var sql = "Select * From " + item.ToString();
                //Console.WriteLine("Read the " + item.ToString());

                try
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(sql);
                    if (searcher.Get().Count != 0)
                    {
                        OuputSection(item.ToString(), ref StrBuilder);
                        foreach (ManagementObject mo in searcher.Get())
                        {
                            if (mo.Properties.Count != 0)
                            {
                                List<GatherHardware1Model> listSubGather = OutputProperites(
                                    item.ToString(),
                                    mo.Properties,
                                    ref StrBuilder
                                );
                                if (listSubGather.Count > 0)
                                {
                                    listGather.InsertRange(listGather.Count, listSubGather);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    del_write?.Invoke("item error ");
                    //Console.WriteLine("item error ");
                }
            }

            //开始组装
            List<string> listP = new List<string>();

            var g1All = listGather.OrderBy(c => c.sortNo).GroupBy(c => new { c.displayText });
            foreach (var g1 in g1All)
            {
                string displayText = g1.Key.displayText;
                List<GatherHardware1Model> list1 = listGather
                    .Where(c => c.displayText == displayText)
                    .OrderBy(c => c.sortNo)
                    .ToList();
                string dbColumnName = list1.Max(c => c.dbColumnName);

                for (int i = list1.Count - 1; i >= 0; i--)
                {
                    var item1 = list1[i];

                    //这里需要二次加工
                    if (item1.sectionName == "Win32_BIOS" && item1.keyName == "ReleaseDate")
                    {
                        string sdtm = item1.keyValue.Substring(0, 8);
                        DateTime dtm = SystemHelper.GetDateTimeByString(sdtm, "yyyyMMdd");

                        string sdtm2 = dtm.ToString(SystemHelper.dateFormat);
                        item1.keyValue = sdtm2;
                    }
                    if (
                        item1.sectionName == "Win32_NetworkAdapterConfiguration"
                        && item1.keyName == "IPAddress"
                    )
                    {
                        if (string.IsNullOrEmpty(item1.keyValue))
                        {
                            list1.RemoveAt(i);
                            continue;
                        }

                        //只保留192.168.1.xxx的
                        if (item1.keyValue.IndexOf("192.168.1.") < 0)
                        {
                            list1.RemoveAt(i);
                            continue;
                        }
                    }
                    else if (
                        item1.sectionName == "Win32_OperatingSystem"
                        && item1.keyName == "TotalVisibleMemorySize"
                    )
                    {
                        double keyValue2 = Convert.ToDouble(item1.keyValue);
                        string keyValue3 = Math.Round(keyValue2 / (1024 * 1024), 2) + "G";

                        item1.keyValue = keyValue3;
                    }
                    else if (item1.sectionName == "Win32_NetworkAdapter")
                    {
                        if (
                            item1.keyValue == "Microsoft"
                            || item1.keyValue.ToLower().IndexOf("vmware") >= 0
                            || item1.keyValue.ToLower().IndexOf("virtual") >= 0
                        )
                            list1.RemoveAt(i);
                    }
                }

                List<string> listSubP = list1.Select(c => c.keyValue).ToList();
                string p = string.Join("->", listSubP);

                listP.Add(displayText + "：" + p);
                //Console.WriteLine(displayText + "：" + p);
                del_write?.Invoke(displayText + "：" + p);
            }

            string ps = string.Join(Environment.NewLine, listP);
            return ps;
        }

        static List<GatherHardware1Model> OutputProperites(
            string item,
            PropertyDataCollection list,
            ref StringBuilder StrBuilder
        )
        {
            List<GatherHardware1Model> listP = new List<GatherHardware1Model>();

            foreach (PropertyData pd in list)
            {
                GatherHardware1Model modelP = SystemHelper.CheckIsGather(item, pd.Name);

                if (pd.Value is string[])
                {
                    StrBuilder.Append(pd.Name + "  ");
                    foreach (string str in pd.Value as string[])
                    {
                        StrBuilder.Append(str + ";   ");
                    }
                    StrBuilder.AppendLine();

                    if (modelP != null)
                    {
                        List<string> listSubP = new List<string>();
                        foreach (string str in pd.Value as string[])
                        {
                            listSubP.Add(str);
                        }
                        string subPs = string.Join(";", listSubP);

                        modelP.keyValue = subPs;
                    }
                }
                else if (pd.Value is UInt16[])
                {
                    StrBuilder.Append(":  ");
                    foreach (int str in pd.Value as UInt16[])
                    {
                        StrBuilder.Append(str + ";  ");
                    }
                    StrBuilder.AppendLine();

                    if (modelP != null)
                    {
                        List<string> listSubP = new List<string>();
                        foreach (int str in pd.Value as UInt16[])
                        {
                            listSubP.Add(str.ToString());
                        }
                        string subPs = string.Join(";", listSubP);

                        modelP.keyValue = subPs;
                    }
                }
                else
                {
                    StrBuilder.AppendLine(pd.Name + ":    " + pd.Value);

                    if (!SystemHelper.ObjectIsNullOrEmpty(pd.Value) && modelP != null)
                    {
                        modelP.keyValue = pd.Value.ToString();
                    }
                }

                ////测试代码
                //if (item == "Win32_OperatingSystem" && pd.Name == "TotalVisibleMemorySize")
                //{
                //    string sss = null;
                //}

                if (modelP != null)
                    listP.Add(modelP);
            }

            return listP;
        }

        static void OuputSection(string name, ref StringBuilder StrBuilder)
        {
            StrBuilder.AppendLine(
                "************************************"
                    + name
                    + "****************************************"
            );
        }

        #region 备份代码，暂时不使用

        ////主板信息
        //public static void GetBaseBoardInfoList(ref StringBuilder StrBuilder)
        //{
        //    OuputSection("BaseBoard", ref StrBuilder);
        //    try
        //    {
        //        ManagementObjectSearcher searcher =
        //new ManagementObjectSearcher("Select * From Win32_BaseBoard");
        //        foreach (ManagementObject mo in searcher.Get())
        //        {
        //            OutputProperites(mo.Properties, ref StrBuilder);
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("GetBaseBoardInfoList error ");
        //    }
        //}


        ////BIOS信息：
        //public static void GetBiosInfo(ref StringBuilder StrBuilder)
        //{
        //    OuputSection("BIOS", ref StrBuilder);
        //    try
        //    {
        //        ManagementObjectSearcher searcher =
        //new ManagementObjectSearcher("Select * From Win32_BIOS");
        //        foreach (ManagementObject mo in searcher.Get())
        //        {
        //            OutputProperites(mo.Properties, ref StrBuilder);
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("GetBiosInfo error ");
        //    }
        //}


        ////物理磁盘信息：
        //public static void GetPhysicalDiskInfo(ref StringBuilder StrBuilder)
        //{
        //    OuputSection("PhysicalDisk", ref StrBuilder);
        //    try
        //    {
        //        ManagementObjectSearcher searcher =
        //new ManagementObjectSearcher("Select * From Win32_DiskDrive");
        //        foreach (ManagementObject mo in searcher.Get())
        //        {
        //            OutputProperites(mo.Properties, ref StrBuilder);
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("GetBiosInfo error ");
        //    }
        //}


        ////网卡配置信息：
        //public static  void GetNAConfigurationInfo(ref StringBuilder StrBuilder)
        //{
        //    OuputSection("NAConfiguration", ref StrBuilder);
        //    try
        //    {
        //        ManagementObjectSearcher searcher =
        //new ManagementObjectSearcher("Select * From Win32_NetworkAdapterConfiguration");
        //        foreach (ManagementObject mo in searcher.Get())
        //        {
        //            OutputProperites(mo.Properties, ref StrBuilder);
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("GetBiosInfo error ");
        //    }
        //}


        ////CPU信息：
        //public static void GetProcessorInfo(ref StringBuilder StrBuilder)
        //{
        //    OuputSection("Processor", ref StrBuilder);
        //    try
        //    {
        //        ManagementObjectSearcher searcher =
        //new ManagementObjectSearcher("Select * From Win32_Processor");
        //        foreach (ManagementObject mo in searcher.Get())
        //        {
        //            OutputProperites(mo.Properties, ref StrBuilder);
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("GetBiosInfo error ");
        //    }
        //}


        ////逻辑磁盘信息：
        //public static void GetLogicalDiskInfo(ref StringBuilder StrBuilder)
        //{
        //    OuputSection("LogicalDisk", ref StrBuilder);
        //    try
        //    {
        //        ManagementObjectSearcher searcher =
        //new ManagementObjectSearcher("Select * From Win32_LogicalDisk");
        //        foreach (ManagementObject mo in searcher.Get())
        //        {
        //            OutputProperites(mo.Properties, ref StrBuilder);
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("GetBiosInfo error ");
        //    }
        //}


        ////进程信息：
        //public static void GetProcessInfo(ref StringBuilder StrBuilder)
        //{
        //    OuputSection("ProcessInfo", ref StrBuilder);
        //    try
        //    {
        //        ManagementObjectSearcher searcher =
        //new ManagementObjectSearcher("Select * From Win32_Process");
        //        ManagementOperationObserver observer = new ManagementOperationObserver();
        //        ObjectReadyHandler handler = new ObjectReadyHandler();
        //        observer.ObjectReady += new ObjectReadyEventHandler(handler.Done);

        //        foreach (ManagementObject mo in searcher.Get())
        //        {
        //            OutputProperites(mo.Properties, ref StrBuilder);
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("GetBiosInfo error ");
        //    }
        //}

        //public class ObjectReadyHandler
        //{
        //    private ManagementBaseObject returnMbo;
        //    private bool operationCompleted = false;

        //    public void Done(object sender, ObjectReadyEventArgs e)
        //    {
        //        this.operationCompleted = true;
        //        this.returnMbo = e.NewObject;
        //    }

        //    public ManagementBaseObject ReturnMbo
        //    {
        //        get
        //        {
        //            return this.returnMbo;
        //        }
        //    }

        //    public bool OperationCompleted
        //    {
        //        get
        //        {
        //            return this.operationCompleted;
        //        }
        //    }
        //}


        ////服务信息：
        //public static void GetServiceInfo(ref StringBuilder StrBuilder)
        //{
        //    OuputSection("Service", ref StrBuilder);
        //    try
        //    {
        //        ManagementObjectSearcher searcher =
        //new ManagementObjectSearcher("Select * From Win32_Service");
        //        foreach (ManagementObject mo in searcher.Get())
        //        {
        //            OutputProperites(mo.Properties, ref StrBuilder);
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("GetBiosInfo error ");
        //    }
        //}

        ////内存信息：
        //public static void GetMemoryInfo(ref StringBuilder StrBuilder)
        //{
        //    OuputSection("Memory", ref StrBuilder);
        //    try
        //    {
        //        ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From CIM_OperatingSystem");
        //        foreach (ManagementObject mo in searcher.Get())
        //        {
        //            OutputProperites(mo.Properties, ref StrBuilder);
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("GetBiosInfo error ");
        //    }
        //}
        #endregion


        /// <summary>
        /// 获取本机所有ip地址
        /// <example>
        /// <code>
        /// SystemHelper.GetLocalIpAddress("InterNetwork");
        /// </code>
        /// <strong>Output: </strong>
        /// 192.168.240.15
        /// </example>
        /// </summary>
        /// <param name="netType">"InterNetwork":ipv4地址，"InterNetworkV6":ipv6地址</param>
        /// <returns>ip地址集合</returns>
        public static List<string> GetLocalIpAddress(string netType)
        {
            string hostName = Dns.GetHostName(); //获取主机名称
            IPAddress[] addresses = Dns.GetHostAddresses(hostName); //解析主机IP地址

            List<string> IPList = new List<string>();
            if (netType == string.Empty)
            {
                for (int i = 0; i < addresses.Length; i++)
                {
                    IPList.Add(addresses[i].ToString());
                }
            }
            else
            {
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                for (int i = 0; i < addresses.Length; i++)
                {
                    if (addresses[i].AddressFamily.ToString() == netType)
                    {
                        IPList.Add(addresses[i].ToString());
                    }
                }
            }
            return IPList;
        }
    }

    public enum HardwareEnum
    {
        // 硬件
        Win32_Processor, // CPU 处理器
        Win32_PhysicalMemory, // 物理内存条

        //Win32_Keyboard, // 键盘
        //Win32_PointingDevice, // 点输入设备，包括鼠标。
        //Win32_FloppyDrive, // 软盘驱动器
        Win32_DiskDrive, // 硬盘驱动器

        //Win32_CDROMDrive, // 光盘驱动器
        Win32_BaseBoard, // 主板
        Win32_BIOS, // BIOS 芯片

        //Win32_ParallelPort, // 并口
        //Win32_SerialPort, // 串口
        //Win32_SerialPortConfiguration, // 串口配置
        Win32_SoundDevice, // 多媒体设置，一般指声卡。
        Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP)
        Win32_USBController, // USB 控制器
        Win32_NetworkAdapter, // 网络适配器
        Win32_NetworkAdapterConfiguration, // 网络适配器设置

        //Win32_Printer, // 打印机
        //Win32_PrinterConfiguration, // 打印机设置
        //Win32_PrintJob, // 打印机任务
        //Win32_TCPIPPrinterPort, // 打印机端口
        //Win32_POTSModem, // MODEM
        //Win32_POTSModemToSerialPort, // MODEM 端口
        Win32_DesktopMonitor, // 显示器
        Win32_DisplayConfiguration, // 显卡
        Win32_DisplayControllerConfiguration, // 显卡设置
        Win32_VideoController, // 显卡细节。
        Win32_VideoSettings, // 显卡支持的显示模式。

        // 操作系统
        Win32_TimeZone, // 时区
        Win32_SystemDriver, // 驱动程序
        Win32_DiskPartition, // 磁盘分区
        Win32_LogicalDisk, // 逻辑磁盘
        Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。

        //Win32_LogicalMemoryConfiguration, // 逻辑内存配置
        //Win32_PageFile, // 系统页文件信息
        //Win32_PageFileSetting, // 页文件设置
        Win32_BootConfiguration, // 系统启动配置
        Win32_ComputerSystem, // 计算机信息简要
        Win32_OperatingSystem, // 操作系统信息

        //Win32_StartupCommand, // 系统自动启动程序
        Win32_Service, // 系统安装的服务
        Win32_Group, // 系统管理组
        Win32_GroupUser, // 系统组帐号
        Win32_UserAccount, // 用户帐号

        //Win32_Process, // 系统进程
        //Win32_Thread, // 系统线程
        //Win32_Share, // 共享
        //Win32_NetworkClient, // 已安装的网络客户端
        //Win32_NetworkProtocol, // 已安装的网络协议
        CIM_OperatingSystem, //内存信息
    }

    public class GatherHardware1Model
    {
        public string dbColumnName { get; set; }
        public string displayText { get; set; }
        public string sectionName { get; set; }
        public string keyName { get; set; }
        public int sortNo { get; set; }

        public string keyValue { get; set; }
    }
}
