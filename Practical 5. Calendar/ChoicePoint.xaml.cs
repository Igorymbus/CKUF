using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Practical_5._Calendar
{
    /// <summary>
    /// Логика взаимодействия для ChoicePoint.xaml
    /// </summary>
    [DataContract]
    public partial class ChoicePoint : UserControl
    {
        [DataMember]
        public string ChoiceName { get; set; }
        [DataMember]
        public Uri Icon { get; set; }
        
        [DataMember]
        public bool isSelected { get; set; }
        public ChoicePoint()
        {
            InitializeComponent();
            DataContext = this;
        }

    }
}
