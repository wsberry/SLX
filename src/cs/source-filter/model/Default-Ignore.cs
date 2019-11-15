// Prologue
//
// SLX - Simple Library Extensions
//
// Copyright 2000-2018 Bill Berry
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// TODO: Creates default setting for files that should be filtered
//       out. 
//       - re-write to use vs ignore and git ignore files.
//       - re-write to be separable and discoverable via 
//         a DLL (load at run time).
//
// endPrologue

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using slx.mvc;

namespace source_filter
{
  /// <summary>
  /// CUSTOM specific code
  /// </summary>
  public class Default_Ignore : IIgnoreData
  {
    private Dictionary<Ignore, List<string>> dataFilters = new Dictionary<Ignore, List<string>>();
    private Dictionary<string, string> contentReplace = new Dictionary<string, string>();

    /// <inheritdoc />
    public Default_Ignore()
    {
      Init();
    }

    public Dictionary<Ignore, List<string>> DataFilters
    {
      get { return dataFilters; }
      set { dataFilters = value; }
    }

    public Dictionary<string, string> ContentReplace
    {
      get { return contentReplace; }
      set { contentReplace = value; }
    }

    public const string DATA_MODEL_NAME = ".my-ignore";

    public string DataModelName => DATA_MODEL_NAME;

    public sealed class keys
    {
      public const string FilesIgnore = "FileFilter";
      public const string FoldersIgnore = "FoldersIgnore";
    }

    public void Init()
    {
      Clear();

      dataFilters.Add(Ignore.File, new List<string>
      {
      });

      dataFilters.Add(Ignore.Directory, new List<string>
      {
        // Create default settings for the user
        //
        { ".vs"},
        {"_build"},
        {"obj"},
        {"bin"},
        {"Debug"},
        {"Release"},
        {"x64"},
        {".git"},
        {"tmp" },
        {"packages" }
      });

      dataFilters.Add(Ignore.Extensions, new List<string>
      {
        { "*.usr"},
        {"*.db"},
        {"*.opendb"},
      });

      // Note that brute force is a whole lot faster than
      // regular expressions here. You will take a huge 
      // performance hit if you try to use regx. Therefore
      // the following implementation:
      //
      contentReplace = new Dictionary<string, string>
      {
        {"\t", @"  "},
      };
    }

    public void Clear()
    {
      dataFilters.Clear();
      contentReplace.Clear();
    }

    public bool IgnoreFileTypeForSearchAndReplace(string file_or_path)
    {
      Debug.Assert(!string.IsNullOrEmpty(file_or_path));

      if (string.IsNullOrEmpty(file_or_path)) return true;

      var ext = Path.GetExtension(file_or_path);

      return !(".cxx" == ext ||
               ".cc" == ext ||
               ".c" == ext ||
               ".h" == ext ||
               ".hpp" == ext ||
               ".hxx" == ext);
    }
  }
}