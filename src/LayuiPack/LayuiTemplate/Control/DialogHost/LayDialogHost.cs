﻿using LayuiTemplate.Dialog;
using LayuiTemplate.Dialog.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LayuiTemplate.Control
{
    public class LayDialogHost : ContentControl
    {
        public LayDialogHost()
        {
            Unloaded += LayDialogHost_Unloaded;
        }
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }


        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(LayDialogHost), new PropertyMetadata(false));

        private void LayDialogHost_Unloaded(object sender, RoutedEventArgs e)
        {
            var tooken = LayDialog.GetTooken(this);
            if (LayDialog.DialogHosts.ContainsKey(tooken)) LayDialog.DialogHosts.Remove(tooken);
        }
    }
}
