﻿// ParseModXIV
// RegExps.cs
//  
// Created by Ryan Wilson.
// Copyright (c) 2010-2012, Ryan Wilson. All rights reserved.

using System.Text.RegularExpressions;

namespace ParseModXIV.Classes
{
    public static class RegExps
    {
        private const RegexOptions DefaultOptions = RegexOptions.Compiled | RegexOptions.ExplicitCapture;

        public static readonly Regex Romans = new Regex(@"(?<roman>\b[IVXLCDM]+\b)", DefaultOptions);

        public static readonly Regex Cicuid = new Regex(@".+=(?<cicuid>\d+)", DefaultOptions);

        public static readonly Regex Commands = new Regex((@"com:(?<cmd>(show-all|\w+)) (?<sub>\w+)"), DefaultOptions);
    }
}