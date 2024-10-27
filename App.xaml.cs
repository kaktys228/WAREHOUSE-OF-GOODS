using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using курсовая_работа.Models;

namespace курсовая_работа
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static KURSEntities1 SR1Entities = new KURSEntities1();
    }
}
