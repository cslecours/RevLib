using System.ComponentModel;

namespace RevLib
{
    public class Player : INotifyPropertyChanged
    {
        public Token Token { get; private set; }

        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

        public string TokenColor { get { return Token.ToString(); } }

        public Player(Token t)
        {
            Token = t;
            Name = Token.ToString();
        }

        public override string ToString()
        {
            return Token.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
