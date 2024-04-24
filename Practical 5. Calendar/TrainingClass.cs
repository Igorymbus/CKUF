using System;
using System.Windows.Media.Imaging;

namespace Practical_5._Calendar
{
    public class TrainingClass
    {
        public readonly string Name;
        public readonly BitmapImage Icon;
        public readonly bool isSelected;
        public TrainingClass()
        {
            Name = "Играть доту";
            Icon = new BitmapImage(new Uri("gantel.png", UriKind.Relative));
            isSelected = false;
        }
    }
}
