using System.Collections.Generic;
using System.Linq;

namespace AppBlocks.Models.Commands
{
    public class LoadAppsCommand : BaseCommand
    {
        public override void Execute(object parameter) => Context.Group = Item.FromService<Item>();//.FirstOrDefault();
    }
}