using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

class AutoShutdown
{
    private static (int, int,int)fanHuiShiJian(int XS, int FZ,int ZJZ)
    {
        int HH;
        int A = ZJZ + FZ; //方便计算小时
        HH =XS + (A - A % 60) / 60;
        XS = HH % 24;
        FZ = A%60;
        return(XS,FZ,ZJZ);
    }
    static void Main(string[] args)
    {
        int newHour;
        int newMinute;
        DateTime now = DateTime.Now;
        bool xunHuan = true;
        Console.WriteLine("自动关机程序");
        Thread.Sleep(500);
        Console.WriteLine("测试版本，版本号ver0.02");
        int xianShi = now.Hour;
        int fenZhon = now.Minute;
        while (xunHuan)
        {
            // 设置关机延迟时间（以秒为单位）
            if (fenZhon < 10)
            {
                Console.WriteLine($"当前时间{xianShi}:0{fenZhon}");
            }
            else
            {
                Console.WriteLine($"当前时间{xianShi}:{fenZhon}");
            }
            Thread.Sleep(250);
            Console.Write("你想要在多少分钟后关机？请输入(请输入正整数)：");
            string huoQu = Console.ReadLine();
            int intHuoQu = int.Parse(huoQu);
            (newHour, newMinute, _) = fanHuiShiJian(xianShi, fenZhon, intHuoQu);
            int delayInSeconds = intHuoQu * 60; //将用户输入改为秒

            // 询问用户是否确认关机
            if (newMinute < 10)
            {
                Console.Write($"现在时间是{xianShi}:{fenZhon},你希望在{huoQu}分钟后关机。 ");
                Console.WriteLine($"也就是在北京时间{newHour}:0{newMinute}关机，如果是,请输入Y");
            }
            else
            {
                Console.Write($"现在时间是{xianShi}:{fenZhon},你希望在{huoQu}分钟后关机。 ");
                Console.WriteLine($"也就是在北京时间{newHour}:{newMinute}关机，如果是,请输入Y");
            }
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "y")
            {
                if (newMinute < 10)
                {
                    Console.WriteLine($"系统将在北京时间{newHour}:0{newMinute}关机!!!");
                }
                else
                {
                    Console.WriteLine($"系统将在北京时间{newHour}:{newMinute}关机!!!");
                }
                

                // 创建一个线程来等待指定的时间，然后关机
                //Thread.Sleep(delayInSeconds * 1000);

                // 使用shutdown命令关机
                ProcessStartInfo startInfo = new ProcessStartInfo("shutdown", $"/s /t {delayInSeconds}");
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;

                try
                {
                    // 启动关机进程
   
                    Process.Start(startInfo);
                    Console.WriteLine("关机命令已发送，计算机将关闭。");
                    //Console.WriteLine("SUCCESS");
                    xunHuan = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"发送关机命令时出错: {ex.Message}");
                    //xunHuan = false;
                }
            }

            else
            {
                Console.WriteLine("确认到输入时间错误，请重新确认。");
                //xunHuan = false;
            }
            Console.WriteLine("关机成功！");
        }
    }
}