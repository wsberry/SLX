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
// TODO: Creates custom setting for files that should be filtered
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
  public class CUSTOM_Ignore : IIgnoreData
  {
    private Dictionary<Ignore, List<string>> dataFilters = new Dictionary<Ignore, List<string>>();
    private Dictionary<string, string> contentReplace = new Dictionary<string, string>();

    /// <inheritdoc />
    public CUSTOM_Ignore()
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

    public const string DATA_MODEL_NAME = ".custom-ignore";

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
        {".catkin_workspace"},
        {".cproject"},
        {".dockerignore"},
        {".git"},
        {".gitignore"},
        {".gitmodules"},
        {".project"},
        {".pydevproject"},
        {".travis.yml"},
        {"AUTHORS"},
        {"CATKIN_IGNORE"},
        {"CHANGLOG.md"},
        {"CONTRIBUTING.md"},
        {"Dockerfile"},
        {"Jenkinsfile"},
        {"LICENSE"},
        {"LICENSE.TXT"},
        {"Makefile"},
        {"README"},
        {"README.md"},
        {"VERSION"},
        {"autonomy_environment_file"},
        {"doxyfile"},
        {"gtri-autonomy.service"},
        {"package.xml"},
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
        { "BrainBoxes"},
        {"ansible"},
        {"bringup"},
        {"CMakeFiles"},
        {"ci"},
        {"cmake"},
        {"common-lisp"},
        {"deprecated"},
        {"docs"},
        {"gtri-uav-ci"},
        {"installer"},
        {"lib"},
        {"python"},
        {"scripts"},
        {"test"},
        {"third_party"},
        {"units_test"},
        {"zlib-1.2.11"},
        {"gtri_uav_radio"},
      });

      dataFilters.Add(Ignore.Extensions, new List<string>
      {
        { "*.usr"},
        {"*.db"},
        { "*.__init__.py"},
        {"*.a"},
        {"*.action"},
        {"*.am"},
        {"*.bash"},
        {"*.catkin"},
        {"*.cs"},
        {"*.in"},
        {"*.js"},
        {"*.json"},
        {"*.log"},
        {"*.ninja"},
        {"*.ninja_deps"},
        {"*.ninja_log"},
        {"*.pc"},
        {"*.py"},
        {"*.pyc"},
        {"*.rosinstall"},
        {"*.sh"},
        {"*.so"},
        {"*.srv"},
        {"*.0"},
        {"*.00"},
        {"*.01"},
        {"*.1"},
        {"*.tmp"},
        {"*.zsh"},
        {"*CATKIN_IGNORE"},
        {"*.rviz"},
        {"*.stamp"},
        {"*.hash"},
      });

      // Note that brute force is a whole lot faster than
      // regular expressions here. You will take a huge 
      // performance hit if you try to use regx. Therefore
      // the following implementation:
      //
      contentReplace = new Dictionary<string, string>
      {
        {@"gtri_formation::", @"gt_formation::"},
        {@"= gtri_formation;", @"= gt_formation;"},
        {@"namespace gtri_formation", @"namespace gt_formation"},
        {"namespace\tgtri_formation", "namespace\tgt_formation"},
        {@"gtri_autonomy::", @"gt_autonomy::"},
        {@"namespace gtri_autonomy", @"namespace gt_autonomy"},
        {"namespace\tgtri_autonomy", "namespace\tgt_autonomy"},
        {@"= gtri_autonomy;", @"= gt_autonomy;"},
        {@" printf", @" safe_cout"},
        {"\tprintf", "\tsafe_cout"},
        {"std::printf", "safe_cout"},
        {@"stdio.h", @"cstdio"},
        {@"math.h", @"cmath"},
        {@"__attribute__((unused))", ""},
        {@"experimental/optional", @"boost/optional/optional.hpp"},
        {@"std::experimental::optional", @"boost::optional"},
        {"\"boost/optional/optional.hpp\"", @"<boost/optional/optional.hpp>"},
        {"#include \"boost/graph/dijkstra_shortest_paths.hpp\"", @"#include <boost/graph/dijkstra_shortest_paths.hpp>"},
        {"#include \"boost/graph/filtered_graph.hpp\"", @"#include <boost/graph/filtered_graph.hpp>"},
        {"#include \"boost/graph/adjacency_list.hpp\"", @"#include <boost/graph/adjacency_list.hpp>"},
        {"#include \"boost/core/ignore_unused.hpp\"", @"#include <boost/core/ignore_unused.hpp>"},
        {"#include \"boost/graph/visitors.hpp\"", @"#include <boost/graph/visitors.hpp>"},
        {"#include \"boost/property_map/property_map.hpp\"", @"#include <boost/property_map/property_map.hpp>"},
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