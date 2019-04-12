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
    public class BinarySerializePrgSet : BinaryAbstractService<ProgrammSettingsModel>
    {
        public BinarySerializePrgSet(BinaryFormatter formatter):base(formatter)
        {
        }

        public override ProgrammSettingsModel Open(string filename)
        {
            ProgrammSettingsModel player=new ProgrammSettingsModel();

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                try
                {
                    player = (ProgrammSettingsModel)_formatter.Deserialize(fs);
                }
                catch(Exception ex)
                {

                }                
            }

            return player;
        }

        public override void Save(string filename, ProgrammSettingsModel playerSet)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                _formatter.Serialize(fs, playerSet);
            }
        }
    }
}
