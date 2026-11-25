using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Book_Managment.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class HideInTableAttribute : Attribute {}
}
