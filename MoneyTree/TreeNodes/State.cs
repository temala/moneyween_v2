using System.Collections.Generic;

namespace MoneyTree
{
    public class State : MoneyTreeNodeBase
    {
        private static State france;
        
        private State(string name) : base(name)
        {
          
        }

        public static State StateOfFrance()
        {
            return france ??= new State("France");
        }
    }
}