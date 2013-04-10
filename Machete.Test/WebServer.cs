﻿#region COPYRIGHT
// File:     WebServer.cs
// Author:   Savage Learning, LLC.
// Created:  2012/06/17 
// License:  GPL v3
// Project:  Machete.Test
// Contact:  savagelearning
// 
// Copyright 2011 Savage Learning, LLC., all rights reserved.
// 
// This source file is free software, under either the GPL v3 license or a
// BSD style license, as supplied with this software.
// 
// This source file is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE. See the license files for details.
//  
// For details please refer to: 
// http://www.savagelearning.com/ 
//    or
// http://www.github.com/jcii/machete/
// 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Machete.Test
{
    // Stops and starts web server for Selenium integration tests
    public class WebServer
    {
        private readonly string physicalPath;
        private readonly int port;
        private readonly string virtualDirectory;
        private Process webServerProcess;

        public WebServer(string physicalPath, int port)
            : this(physicalPath, port, "")
        {
        }

        public WebServer(string physicalPath, int port, string virtualDirectory)
        {
            this.port = port;
            this.physicalPath = physicalPath.TrimEnd('\\');
            this.virtualDirectory = virtualDirectory;
        }

        public void Start()
        {
            webServerProcess = new Process();
            const string webDevServerPath = @"C:\Program Files (x86)\Common Files\Microsoft Shared\DevServer\10.0\WebDev.WebServer40.exe";
            string arguments = string.Format("/port:{0} /path:\"{1}\" /vpath:{2}", port, physicalPath, virtualDirectory);
            webServerProcess.StartInfo = new ProcessStartInfo(webDevServerPath, arguments);
            webServerProcess.Start();
        }

        public void Stop()
        {
            if (webServerProcess == null)
            {
                throw new InvalidOperationException("Start() must be called before Stop()");
            }
            webServerProcess.Kill();
        }
    }
}