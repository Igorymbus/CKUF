using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace Practical_5._Calendar
{
    [DataContract]
    public class UserChoicePerDay
    {
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public List<ChoicePoint> userChoice_list { get; set; }

    }
}
