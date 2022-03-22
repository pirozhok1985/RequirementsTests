using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RequirementsTests.Avalonia_MVVM.Command;
using RequirementsTests.Services.UseCases.LinuxInfo;

namespace RequirementsTests.Avalonia_MVVM.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private DelegateCommand _ExitButtonCommand = null;

        public DelegateCommand ExitButtonCommand =>
            _ExitButtonCommand ?? (_ExitButtonCommand = new DelegateCommand(OnExecuteExitButton, null));
        
        public string Greeting => "Welcome to Avalonia!";
        
        public HardwareInfo HardwareInfo { get; set; }
        public OperatingSystemInfo OperatingSystemInfo { get; set; }
        public CpuInfoViewModel CpuInfoViewModel { get; set; }

        private async void OnExecuteExitButton(object? parameter)
        {
            HardwareInfo = new HardwareInfo();
            var result = await HardwareInfo.GetCpuInfoAsync();
            CpuInfoViewModel = new CpuInfoViewModel()
            {
                Model = result?.Value?.Model,
                CoresCount = result!.Value!.CoresCount.ToString()
            };
        }
    }
}