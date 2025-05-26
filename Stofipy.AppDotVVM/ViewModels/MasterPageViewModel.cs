using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace Stofipy.AppDotVVM.ViewModels
{
    public class MasterPageViewModel : DotvvmViewModelBase
    {
        private bool _forceDataRefresh = true;

        public async Task OnAppearingAsync()
        {
            if (_forceDataRefresh)
            {
                await LoadDataAsync();

                _forceDataRefresh = false;
            }
        }

        protected virtual Task LoadDataAsync()
            => Task.CompletedTask;


    }
}
