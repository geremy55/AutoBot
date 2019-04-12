using AutoBot.Interfaces;
using AutoBot.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace AutoBot.Services
{
    class JsonFileService : IFileService<ProgrammSettingsModel>
    {
        public ProgrammSettingsModel Open(string filename)
        {
            var settings = new ProgrammSettingsModel();
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(ProgrammSettingsModel));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                settings = jsonFormatter.ReadObject(fs) as ProgrammSettingsModel;
            }
            return settings;
        }

        public void Save(string filename, ProgrammSettingsModel settings)
        {
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(ProgrammSettingsModel));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, settings);
            }
        }
    }   
}
