﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommonServiceLocator;
using MatrixHammingCipher.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;


namespace MatrixHammingCipher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        #region Overrides of PrismApplicationBase

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //throw new NotImplementedException();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<GilbertMourEncoding.GilbertMourEncodingModule>();
            moduleCatalog.AddModule<CodeMatrix.CodeMatrixModule>();
            moduleCatalog.AddModule<HammingCorrector.HammingCorrectorModule>();
        }

        #endregion
    }
}
