using System.IO;
using System.Windows.Forms;

namespace slx
{
   public static class drag_drop_extensions
   {
      public static void DropFolder(this TextBox tb, DragEventArgs e)
      {
         var paths = (string[])e.Data.GetData(DataFormats.FileDrop);

         if (paths.Length <= 0 || string.IsNullOrEmpty(paths[0]) || !Directory.Exists(paths[0])) return;
         tb.Text = paths[0];
         tb.SelectionLength = 0;
      }
   }
}