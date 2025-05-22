using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stofipy.App.ViewModels;

namespace Stofipy.App.Views;

public partial class SectionBottom
{
    public SectionBottom(FilesInQueueVM viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}