using System.Collections.Generic;

namespace slx.mvc
{
  // TODO: Think through this naming...
  //
  public enum Ignore
  {
    Directory,
    File,
    Extensions
  }

  public interface IIgnoreData
  {
    Dictionary</*directory, file, or extension*/Ignore, /*list of one of the key items:*/List<string>> DataFilters
    {
      get;
      set;
    }

    /// <summary>
    /// Defines content to be replaced
    /// </summary>
    Dictionary</*find:*/string, /*replace:*/string> ContentReplace { get; set; }

    /// <summary>
    /// The name of the data model
    /// </summary>
    string DataModelName { get; }

    void Init();

    void Clear();

    bool IgnoreFileTypeForSearchAndReplace(string file_or_path);
  }
}