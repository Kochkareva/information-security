using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity.Lifetime;
using Unity;
using Contracts.BusinessLogicContracts;
using BusinessLogics;
using Contracts.StoragesContracts;
using FileImplement.Implements;

namespace IB_LW_1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer container = null;

        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            currentContainer.RegisterType<IUserLogic, UserLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IUserStorage, UserStorage>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
        protected override void OnExit(ExitEventArgs e)
        {
            FileImplement.FileDataListSingleton.SaveFileDataListSingleton();
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Здесь вы можете разместить код для запуска вашего приложения
            // Создайте экземпляр вашего главного окна (MainWindow) и запустите его
            FileImplement.FileDataListSingleton.GetFileDataListSingleton();
        }
    }
}
