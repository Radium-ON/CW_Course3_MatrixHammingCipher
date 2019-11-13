﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace MatrixHammingCipher.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            var magnify = Geometry.Parse("F1 M 23.6506,56.2021C 22.5867,57.266 20.8618,57.266 19.7979,56.2021C 18.734,55.1382 18.734,53.4133 19.7979,52.3494L 27.6722,44.4751C 26.6112,42.7338 26,40.6883 26,38.5C 26,32.1487 31.1487,27 37.5,27C 43.8513,27 49,32.1487 49,38.5C 49,44.8513 43.8513,50 37.5,50C 35.3117,50 33.2662,49.3888 31.5249,48.3278L 23.6506,56.2021 Z M 37.5,31C 33.3579,31 30,34.3579 30,38.5C 30,42.6421 33.3579,46 37.5,46C 41.6421,46 45,42.6421 45,38.5C 45,34.3579 41.6421,31 37.5,31 Z ");
            var grid = Geometry.Parse(
                "F1 M 57,19L 57,26L 50,26L 50,19L 57,19 Z M 48,19L 48,26L 41,26L 41,19L 48,19 Z M 39,19L 39,26L 32,26L 32,19L 39,19 Z M 57,28L 57,35L 50,35L 50,28L 57,28 Z M 48,28L 48,35L 41,35L 41,28L 48,28 Z M 39,28L 39,35L 32,35L 32,28L 39,28 Z M 57,37L 57,44L 50,44L 50,37L 57,37 Z M 48,37L 48,44L 41,44L 41,37L 48,37 Z M 39,37L 39,44L 32,44L 32,37L 39,37 Z ");
            LogoData = Geometry.Combine(magnify,grid,GeometryCombineMode.Union,Transform.Identity);
        }

        #region Backing Fields
        private string _title = "Курышев Р. В. БПИ-311";
        private Geometry _logodata;

        

        #endregion

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public Geometry LogoData
        {
            get { return _logodata; }
            set { SetProperty(ref _logodata, value); }
        }
    }
}
