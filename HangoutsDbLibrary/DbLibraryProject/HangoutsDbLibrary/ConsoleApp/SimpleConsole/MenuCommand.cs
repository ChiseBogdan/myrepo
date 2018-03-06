using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoriesAndUnitOfWork.Console
{
    public class MenuCommand : ICommand
    {
        private Dictionary<String, ICommand> dict = new Dictionary<String, ICommand>();
        private String numeMeniu;

        public MenuCommand(String numeMeniu)
        {
            this.numeMeniu = numeMeniu;
        }

        public void Execute()
        {
            foreach (String key in dict.Keys)
                System.Console.WriteLine(key);
        }

        public void addCommand(String descriere, ICommand c)
        {
            dict.Add(descriere, c);
        }

        public int size()
        {
            return dict.Count;
        }

        public List<ICommand> getAllCommands()
        {
            List<ICommand> list = new List<ICommand>();
            foreach (ICommand command in dict.Values)
            {
                list.Add(command);
            }
            return list;
        }

        public String getMenuNume()
        {

            return numeMeniu;
        }

    }
}
