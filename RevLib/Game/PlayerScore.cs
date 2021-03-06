﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace RevLib
{
    public class PlayerScore : INotifyPropertyChanged
    {
        private int score;
        public int Score { 
            get { return score; } 
            set { score = value; OnPropertyChanged("Score"); }
        }

        public PlayerScore Clone()
        {
            return new PlayerScore() { Score = Score };
        }

        void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
