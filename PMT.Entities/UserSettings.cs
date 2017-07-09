using PMT.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Entities
{
    public class UserSettings
    {
        [Key]
        public string UserId { get; set; }

        [Display(Name = nameof(ModelText.UserSettingsTheme), ResourceType = typeof(ModelText))]
        public string Theme { get; set; }

        [Display(Name = nameof(ModelText.UserSettingsItemsPerPage),ResourceType =typeof(ModelText))]
        public int ItemsPerPage { get; set; }

        [Display(Name = nameof(ModelText.UserSettingsDisplayLanguage), ResourceType = typeof(ModelText))]
        public string DisplayLanguage { get; set; }

        [Display(Name = nameof(ModelText.UsersSettingsLanguageFormatting), ResourceType = typeof(ModelText))]
        public string LanguageFormatting { get; set; }
    }
}
