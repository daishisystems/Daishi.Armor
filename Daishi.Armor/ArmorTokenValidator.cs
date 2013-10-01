using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daishi.Armor
{
    public class ArmorTokenValidator: ICommand
    {
        private readonly string securedArmorToken;

        public object Result { get { return IsValid; }}
        public bool IsValid { get;  private set; }

        public ArmorTokenValidator(string securedArmorToken) {
            this.securedArmorToken = securedArmorToken;
        }

        public void Execute() {
            


        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}
