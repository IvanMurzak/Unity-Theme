using System.Linq;

namespace Unity.Theme
{
#pragma warning disable CA2235 // Mark all non-serializable fields
    public partial class Theme
    {
        private bool SortColors(ThemeData theme)
        {
            var changed = false;
            for (int i = 0; i < colors.Count; i++)
            {
                if (theme.colors[i].Guid != colors[i].Guid)
                {
                    var colorData = theme.colors.First(colorData => colorData.Guid == colors[i].Guid);
                    var colorIndex = theme.colors.IndexOf(colorData);
                    
                    var temp = theme.colors[i];
                    theme.colors[i] = theme.colors[colorIndex];
                    theme.colors[colorIndex] = temp;

                    changed = true;
                }
            }
            return changed;
        }
    }
#pragma warning restore CA2235 // Mark all non-serializable fields
}