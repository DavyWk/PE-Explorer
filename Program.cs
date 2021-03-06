﻿using System;
using System.IO;

using Parsers;
using Core;
using Utilities;

namespace PE_Explorer
{
    class Program
    {
        static void Main(string[] args)
        {
            PortableExecutable pe = null;
            BinaryReader br = null;

            Console.Title = "PE Explorer";
            Logger.Log(ELogTypes.Info, "Copyright (c) 2014 DavyWk : https://github.com/DavyWk");
            Console.WriteLine();

            if (args.Length != 1)
            {
                Logger.Log(ELogTypes.Error, "Usage : \"PE Explorer pathtoPEfile\" ");
                Exit();
            }

            var fi = new FileInfo(args[0]);
            Logger.Log(ELogTypes.Info, string.Format("Loading {0}", fi.Name));
            Logger.Log(ELogTypes.Info, string.Format("File size : {0} bytes", fi.Length));

            try
            {
                br = new BinaryReader(File.OpenRead(args[0]));
            }
            catch (IOException ex)
            {
                Logger.Log(ELogTypes.Error, "Error while opening the file : ");
                Logger.Log(ELogTypes.Error, ex.Message);
                Exit();
            }

            try
            {
                pe = new PortableExecutable(br);
            }
            catch (Exception ex)
            {
                if (ex is NotSupportedException || ex is BadImageFormatException)
                {
                    Logger.Log(ELogTypes.Error, ex.Message);
                    Exit();
                }
                else
                    throw;
            }

             new PortableExecutableParser(pe).Parse();


             Console.ReadLine();
        }

        private static void Exit(int exitCode = 1)
        { // lazyness @ its best
            Console.ReadLine();
            Environment.Exit(exitCode);
        }

    }
}
