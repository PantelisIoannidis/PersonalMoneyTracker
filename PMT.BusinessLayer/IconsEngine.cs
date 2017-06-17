using Microsoft.Extensions.Logging;
using PMT.Common;
using PMT.DataLayer.Repositories;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.BusinessLayer
{
    public class IconsEngine : BaseEngine, IIconsEngine
    {
        ILogger logger;
        IActionStatus actionStatus;
        IIconRepository iconRepository;

        public IconsEngine(ILoggerFactory logger,
                                        IActionStatus actionStatus,
                                        IIconRepository iconRepository
                                        )
        {
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<IconsEngine>();
            this.iconRepository = iconRepository;
        }

        public IEnumerable<Icon> GetAll()
        {
            return iconRepository.GetAll();
        }
    }
}
