﻿// Copyright (c) to owners found in https://github.com/AArnott/pinvoke/blob/master/COPYRIGHT.md. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.

using System;
using PInvoke;
using Xunit;
using Xunit.Abstractions;
using static PInvoke.Kernel32;
using static PInvoke.Userenv;

public class UserenvFacts
{
    private readonly ITestOutputHelper logger;

    public UserenvFacts(ITestOutputHelper logger)
    {
        this.logger = logger;
    }

    [Fact]
    public unsafe void CreateEnvironmentBlock_DestroyEnvironmentBlock()
    {
        char* environmentBlock;
        if (!CreateEnvironmentBlock(out environmentBlock, SafeObjectHandle.Null, false))
        {
            throw new Win32Exception();
        }

        Assert.True(environmentBlock != null);
        char* pos = environmentBlock;
        while (*pos != 0)
        {
            string line = new string(pos);
            this.logger.WriteLine(line);

            pos += line.Length + 1;
        }

        if (!DestroyEnvironmentBlock(environmentBlock))
        {
            throw new Win32Exception();
        }
    }
}
