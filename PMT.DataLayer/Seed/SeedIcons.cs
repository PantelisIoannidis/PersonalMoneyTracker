using PMT.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace PMT.DataLayer.Seed
{
    public class SeedIcons
    {

        public void AddFontFromCssToList(List<Icon> icons, string cssPath, string prefix
                            , string collection = "", string afterName = ":before", string finalizeString = "content:")
        {
            try
            {
                string beforeName = "." + prefix;

                var pathFullname = HostingEnvironment.MapPath(cssPath);
                var lines = File.ReadAllLines(pathFullname);
                string iconName = "";
                string line = "";
                for (int i = 0; i < lines.Count(); i++)
                {
                    line = lines[i];
                    if (!line.Contains(beforeName) || !line.Contains(afterName)) continue;
                    iconName = line.Split('.')[1]
                                    .Split(new string[] { afterName }, StringSplitOptions.None)[0]
                                    .Trim();
                    if (collection == "fontawesome") line = lines[i + 1];

                    if (line.Contains(finalizeString) && !string.IsNullOrEmpty(iconName))
                    {
                        icons.Add(new Icon()
                        {
                            IconId = iconName,
                            Name = iconName.Replace(prefix, ""),
                            IsFontAwesome = collection == "fontawesome",
                            IsWebHostingHubGlyphs = collection == "whhg"
                        });
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

