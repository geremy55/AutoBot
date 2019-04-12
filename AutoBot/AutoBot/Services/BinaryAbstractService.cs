using AutoBot.Interfaces;
using AutoBot.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Services
{
    public abstract class BinaryAbstractService<T> : IFileService<T>
    {
        protected BinaryFormatter _formatter;
        public BinaryAbstractService(BinaryFormatter formatter)
        {
            _formatter = formatter;
        }

        public virtual T Open(string filename)
        {
            T player;

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                player = (T)_formatter.Deserialize(fs);
            }

            return player;
        }

        public virtual void Save(string filename, T playerSet)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                _formatter.Serialize(fs, playerSet);
            }
        }
    }
}
