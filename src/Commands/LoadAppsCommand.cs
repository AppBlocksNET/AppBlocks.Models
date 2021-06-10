namespace AppBlocks.Models.Commands
{
    public class LoadAppsCommand : BaseCommand
    {
        public override void Execute(object parameter) => App.Group = new Item();//.FromService<Item>();
    }
}