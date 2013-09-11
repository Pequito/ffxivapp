﻿// FFXIVAPP.Client
// NPCWorkerDelegate.cs
// 
// © 2013 Ryan Wilson

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using FFXIVAPP.Client.Helpers.SocketIO;
using FFXIVAPP.Client.Memory;
using FFXIVAPP.Client.ViewModels;
using FFXIVAPP.Common.Helpers;

#endregion

namespace FFXIVAPP.Client.Delegates
{
    internal static class NPCWorkerDelegate
    {
        #region Declarations

        public static readonly List<NPCEntry> NPCList = new List<NPCEntry>();
        private static readonly UploadHelper UploadHelper = new UploadHelper("import_npc", 1);

        #endregion

        /// <summary>
        /// </summary>
        public static void OnNewNPC(NPCEntry npcEntry)
        {
            Func<bool> saveToDictionary = delegate
            {
                var current = NPCList.Any() ? NPCList.ToList() : new List<NPCEntry>();
                if (current.Any(n => n.NPCID == npcEntry.NPCID))
                {
                    return false;
                }
                NPCList.Add(npcEntry);
                return true;
            };
            saveToDictionary.BeginInvoke(delegate
            {
                var chunkSize = UploadHelper.ChunkSize;
                var chunksProcessed = UploadHelper.ChunksProcessed;
                if (NPCList.Count <= (chunkSize * (chunksProcessed + 1)))
                {
                    return;
                }
                if (!UploadHelper.Processing)
                {
                    UploadHelper.ProcessUpload(new List<NPCEntry>(NPCList.Skip(chunksProcessed * chunkSize)));
                }
            }, saveToDictionary);
        }
    }
}
