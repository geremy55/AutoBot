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
    class BinarySerializeUserSet : BinaryAbstractService<PlayerSettingsModel>
    {
        public BinarySerializeUserSet(BinaryFormatter formatter) : base(formatter)
        {
        }

        public override PlayerSettingsModel Open(string filename)
        {
            PlayerSettingsModel player=new PlayerSettingsModel();

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                try
                {
                    player = (PlayerSettingsModel)_formatter.Deserialize(fs);
                }
                catch(Exception ex)
                {

                }                
            }

            return player;
        }

        public override void Save(string filename, PlayerSettingsModel playerSet)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                _formatter.Serialize(fs, playerSet);
            }
        }
    }
}
