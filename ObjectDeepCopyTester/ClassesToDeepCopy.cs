using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDeepCopyTester
{
    public class Node
    {
        public string name { get; set; }
        public int no;
        public Node next; // For circular reference test
    }
    

}
